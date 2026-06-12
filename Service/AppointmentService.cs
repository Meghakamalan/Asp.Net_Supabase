using Project_sem2.Models;

namespace Project_sem2.Service
{
    public class AppointmentService
    {
        public AppointmentDbContext context;

        public AppointmentService(AppointmentDbContext context_)
        {
            context = context_;
        }
        
        public List<Client> ReadClient()
        {
            return context.clientList.ToList();
        }

        public void AddClient(Client client)
        {
            context.clientList.Add(client);
            context.SaveChanges();
        }

        public void EditClient(Client client)
        {
            context.clientList.Update(client);
            context.SaveChanges();
        }

        public void DeleteClient(Client client)
        {

            var result = context.clientList.FirstOrDefault(c => c.Id == client.Id);
            context.clientList.Remove(result);
            context.SaveChanges();
        }
    }

    
}
