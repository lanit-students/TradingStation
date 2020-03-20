namespace IDeleteUserUserService.Interfaces
{
    
        public interface IDeleteUser<in Input,  out Output>
        {
            Output Execute(Input userId);
        }
}
