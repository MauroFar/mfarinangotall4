namespace mfarinangotall4
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            ContactDatePicker.Date = DateTime.Today;
        }

        private void OnIdentificationTypeChanged(object? sender, EventArgs e)
        {
            IdentificationEntry.Text = string.Empty;
            IdentificationEntry.Keyboard = IdentificationTypePicker.SelectedItem?.ToString() == "Pasaporte"
                ? Keyboard.Default
                : Keyboard.Numeric;
        }

        private async void OnSaveClicked(object? sender, EventArgs e)
        {
            if (!decimal.TryParse(SalaryEntry.Text, out var salary))
            {
                await DisplayAlertAsync("Validación", "El salario debe ser un número válido.", "Aceptar");
                return;
            }

            var contact = new Contact
            {
                IdentificationType = IdentificationTypePicker.SelectedItem?.ToString() ?? string.Empty,
                Identification = IdentificationEntry.Text?.Trim() ?? string.Empty,
                FirstName = FirstNameEntry.Text?.Trim() ?? string.Empty,
                LastName = LastNameEntry.Text?.Trim() ?? string.Empty,
                Date = ContactDatePicker.Date ?? DateTime.Today,
                Email = EmailEntry.Text?.Trim() ?? string.Empty,
                Salary = salary
            };

            var errors = contact.Validate();
            if (errors.Count > 0)
            {
                await DisplayAlertAsync("Revise los datos", string.Join(Environment.NewLine, errors), "Aceptar");
                return;
            }

            ContactStore.Current = contact;
            await Shell.Current.GoToAsync(nameof(ContactSummaryPage));
        }
    }
}
