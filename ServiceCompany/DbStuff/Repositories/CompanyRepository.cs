using Microsoft.EntityFrameworkCore;
using ServiceCompany.Models;
using ServiceCompany.DbStuff.Models;

namespace ServiceCompany.DbStuff.Repositories
{
    public class CompanyRepository : BaseRepository<Company>
    {
        public CompanyRepository(ServiceCompanyDbContext context) : base(context) { }

        public IEnumerable<Company> GetCompaniesWithProfile()
        {
            return _entities
                .Include(x=>x.Profile)
                .Include(x=>x.Author)
                .ThenInclude(x=>x.Profile)
                .ToList();
        }

        public Company GetCompanyWithProfileById(int id)
        {
            return _entities
                .Include(x => x.Profile)
                .First(x => x.Id == id);
        }

        public Company GetTestCompany()
        {
            return _entities
                .Include(x => x.Profile)
                .FirstOrDefault(x => x.Name == "TestCompany");
        }

        public void UpdateCompany(CompanyViewModel viewModel, int id, int statusId)
        {
            var company = _context.Companies
                .Include(x => x.Profile)
                .First(x => x.Id == id);

            company.Name = viewModel.CompanyName;
            company.ShortName = viewModel.CompanyShortName;
            company.Profile.Address = viewModel.CompanyAdress;
            company.Profile.Email = viewModel.CompanyEmail;
            company.Profile.PhoneNumber = viewModel.CompanyPhoneNumber;

            _context.SaveChanges();
        }
    }
}
