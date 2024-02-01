namespace Core.MessageObjects
{
    internal class RegisterNewVehicleOutputMessage
    {
        public string RegistrationNumber { get; set; }

        public string Message { get; set; }

        public OutputMessageError Error { get; set; }
    }
}
