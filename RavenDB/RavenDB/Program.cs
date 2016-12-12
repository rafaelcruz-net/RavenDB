using Raven.Client.Document;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RavenDB
{
    class Program
    {
        static void Main(string[] args)
        {
            var documentStore = new DocumentStore();
            documentStore.Url = "http://localhost:8080";
            documentStore.Initialize();

            //Cria a base de dados caso necessario
            documentStore.DatabaseCommands.GlobalAdmin.EnsureDatabaseExists("Post");

            //Armazena o Id para posteriormente seguir o exemplo de alteração e delete
            var idAuthor = Guid.NewGuid();

            //Cria um novo documento
            using (var session = documentStore.OpenSession("Post"))
            {

                var author = new Author()
                {
                    Id = idAuthor,
                    Email = "xpto@teste.com.br",
                    Name = "Rafael Cruz",
                    Post = new List<Post>() {
                        new Post()
                        {
                            Id = Guid.NewGuid(),
                            Article = "Lorem ipsum",
                            DtPost = DateTime.Now,
                            Title = "Novo Artigo de Teste",
                            Comments = new List<Comment>()
                            {
                                new Comment()
                                {
                                    Title = "Novo Comentário",
                                    Text = "Lorem Ipsum",
                                    CommentUser = "User Comment",
                                    DtComment = DateTime.Now,
                                    Id = Guid.NewGuid(),
                                }

                            }

                        }
                    }
                };

                session.Store(author);
                session.SaveChanges();
            }

            //Alterando uma ou mais propriedades
            using (var session = documentStore.OpenSession("Post"))
            {
                // usamos o método Load para recuperar um documento pelo Id
                var author = session.Load<Author>($"authors/{idAuthor}");

                author.Name = "Rafael Cruz Alterado";
                session.SaveChanges();
            }

            //Deletando um documento
            using (var session = documentStore.OpenSession("Post"))
            {
                // usamos o método Load para recuperar um documento pelo Id
                var author = session.Load<Author>($"authors/{idAuthor}");

                session.Delete<Author>(author);
                session.SaveChanges();
            }
                        
            documentStore.Dispose();

            Console.WriteLine("Operação concluída");
            Console.ReadKey();

        }
    }
}
