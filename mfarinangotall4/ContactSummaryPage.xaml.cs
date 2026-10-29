using System.Globalization;

namespace mfarinangotall4;

public partial class ContactSummaryPage : ContentPage
{
    private Contact? _contact;

    public ContactSummaryPage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _contact = ContactStore.Current;

        if (_contact is null)
            return;

        IdentificationTypeLabel.Text = $"Tipo de identificación: {_contact.IdentificationType}";
        IdentificationLabel.Text = $"Identificación: {_contact.Identification}";
        FirstNameLabel.Text = $"Nombres: {_contact.FirstName}";
        LastNameLabel.Text = $"Apellidos: {_contact.LastName}";
        DateLabel.Text = $"Fecha: {_contact.Date.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)}";
        EmailLabel.Text = $"Correo: {_contact.Email}";
        SalaryLabel.Text = $"Salario: {_contact.Salary.ToString("C2", CultureInfo.CurrentCulture)}";
        ContributionLabel.Text = $"Aporte personal al IESS (9.45%): {_contact.IessContribution.ToString("C2", CultureInfo.CurrentCulture)}";
    }

    private async void OnExportClicked(object? sender, EventArgs e)
    {
        if (_contact is null)
            return;

        try
        {
            var filePath = Path.Combine(FileSystem.CacheDirectory, "contacto_iess.txt");
            await File.WriteAllTextAsync(filePath, _contact.ToText());
            await Share.Default.RequestAsync(new ShareFileRequest
            {
                Title = "Exportar contacto",
                File = new ShareFile(filePath)
            });
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Error al exportar", $"No se pudo crear el archivo: {ex.Message}", "Aceptar");
        }
    }

    private async void OnBackClicked(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}
