using System.Globalization;

namespace mfarinangotall4;

public sealed class Contact
{
    public string IdentificationType { get; set; } = string.Empty;
    public string Identification { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime Date { get; set; } = DateTime.Today;
    public string Email { get; set; } = string.Empty;
    public decimal Salary { get; set; }

    public decimal IessContribution => Math.Round(Salary * 0.0945m, 2);

    public IReadOnlyList<string> Validate()
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(IdentificationType))
            errors.Add("Seleccione el tipo de identificación.");
        if (string.IsNullOrWhiteSpace(Identification))
            errors.Add("Ingrese la identificación.");
        else if (IdentificationType == "CI" && (Identification.Length != 10 || !Identification.All(char.IsDigit)))
            errors.Add("La CI debe contener exactamente 10 números.");
        else if (IdentificationType == "RUC" && (Identification.Length != 13 || !Identification.All(char.IsDigit)))
            errors.Add("El RUC debe contener exactamente 13 números.");

        if (string.IsNullOrWhiteSpace(FirstName))
            errors.Add("Ingrese los nombres.");
        if (string.IsNullOrWhiteSpace(LastName))
            errors.Add("Ingrese los apellidos.");
        if (string.IsNullOrWhiteSpace(Email) || !IsValidEmail(Email))
            errors.Add("Ingrese un correo electrónico válido.");
        if (Salary <= 0)
            errors.Add("El salario debe ser mayor que cero.");

        return errors;
    }

    private static bool IsValidEmail(string email)
    {
        return email.Contains('@', StringComparison.Ordinal) &&
               email.Contains('.', StringComparison.Ordinal) &&
               !email.Contains(' ', StringComparison.Ordinal);
    }

    public string ToText()
    {
        return string.Join(Environment.NewLine,
            "REGISTRO DE CONTACTO",
            $"Tipo de identificación: {IdentificationType}",
            $"Identificación: {Identification}",
            $"Nombres: {FirstName}",
            $"Apellidos: {LastName}",
            $"Fecha: {Date.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)}",
            $"Correo: {Email}",
            $"Salario: {Salary.ToString("C2", CultureInfo.CurrentCulture)}",
            $"Aporte personal IESS (9.45%): {IessContribution.ToString("C2", CultureInfo.CurrentCulture)}");
    }
}

public static class ContactStore
{
    public static Contact? Current { get; set; }
}
