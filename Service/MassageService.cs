using Project_sem2.Models;

namespace Project_sem2.Service
{
    public class MassageService
    {
        public AppointmentDbContext context;

        public MassageService(AppointmentDbContext context_)
        {
            context = context_;
        }
        
        public List<Massage> ReadMassage()
        {
            return context.massageList.ToList();
        }

        public void AddMassage(Massage massage)
        {
            context.massageList.Add(massage);
            context.SaveChanges();
        }

        public void EditMassage(Massage massage)
        {
            context.massageList.Update(massage);
            context.SaveChanges();
        }

        public void DeleteMassage(Massage massage)
        {

            var result = context.massageList.FirstOrDefault(m => m.Id == massage.Id);
            context.massageList.Remove(result);
            context.SaveChanges();
        }
    }

    
}
