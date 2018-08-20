using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StatementViewer.Services;
using StatementViewer.Vendors;

namespace StatementViewer.Tests
{
    [TestClass]
    public class VendorRepositoryTests
    {
        VendorRepository _repo = new VendorRepository(@"D:\Dev\Database\home.db");
        [TestMethod]
        public void VendorExists_WhenVendorExists_ReturnsTrueAndDescriptionIsNotNull()
        {
            Vendor existingVendor = _repo.GetVendors().FirstOrDefault();
            Assert.IsTrue(existingVendor != null);
            Assert.IsTrue(_repo.VendorExists(existingVendor));            
        }
    }
}
