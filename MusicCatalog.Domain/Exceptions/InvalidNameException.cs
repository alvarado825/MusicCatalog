namespace MusicCatalog.Domain.Exceptions
{
    public class InvalidNameException : DomainException
    {
        public InvalidNameException() : base("Name não pode ser vazio, nulo ou conter somente espaços em branco"){}      

        public InvalidNameException(string message) : base(message){} 
    }
}