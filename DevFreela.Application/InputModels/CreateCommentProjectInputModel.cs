namespace DevFreela.Application.InputModels
{
    public class CreateCommentProjectInputModel
    {
        public string Content { get; set; }
        public int IdProject { get; set; }
        public int IdUser { get; set; }
    }
}
