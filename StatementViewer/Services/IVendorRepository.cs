using StatementViewer.Vendors;
using System.Collections.Generic;

namespace StatementViewer.Services
{
    public interface IVendorRepository
    {
        bool AddVendor(Vendor vendor);
        bool DeleteVendor(int vendorId);
        Vendor GetVendor(int tvendorId);
        IEnumerable<Vendor> GetVendors();
        bool SetDatabaseConnectionString(string path);
        void UpdateVendor(Vendor vendor);
    }
}
