using VeriTakipMvc.Data.Interfaces;
using VeriTakipMvc.Models;

namespace VeriTakipMvc.Data.Repository
{
	public class CompanyRepository : ICompanyRepository
	{

		private readonly Context _context;

		public CompanyRepository(Context context)
		{
			_context = context;
		}

		public Task CreateCompany(Company company)
		{
			_context.Companies.Add(company);
			return _context.SaveChangesAsync();
		}

		public Task DeleteCompany(int id)
		{

			var company = _context.Companies.Find(id);
			_context.Companies.Remove(company);
			return _context.SaveChangesAsync();

		}

		public async Task<Company> GetCompany(int id)
		{
			return await _context.Companies.FindAsync(id).AsTask();
		}

		public Task UpdateCompany(Company company)
		{
			_context.Companies.Update(company);
			return _context.SaveChangesAsync();
		}
	}
}
