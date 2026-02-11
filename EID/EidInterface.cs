using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EID
{
    public interface IBusinessAssociate
    {
        /// <summary>
        /// Gets the name of the business associate.
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// Gets or sets the unique identifier for the entity.
        /// </summary>
        int Id { get; }
        /// <summary>
        /// Gets or sets the total amount associated with the business associate.
        /// </summary>
        double TotalAmount { get; set; }
    }
    /// <summary>
    /// Represents a customer with a unique identifier and a name. This is an abstract base class for customer-related
    /// entities.
    /// </summary>
    /// <remarks>Inherit from this class to implement specific types of customers with additional properties
    /// or behaviors. This class provides common properties shared by all customer types.</remarks>
    public abstract class Customer
    {
        /// <summary>
        /// Gets or sets the unique identifier for the customer.
        /// </summary>
        public int CustomerId { get; set; }
        /// <summary>
        /// Gets or sets the name associated with the object.
        /// </summary>
        public string Name { get; set; }
    }
    /// <summary>
    /// Represents an individual consumer who is a customer and a business associate.
    /// </summary>
    /// <remarks>This class extends the Customer type and implements the IBusinessAssociate interface,
    /// providing additional identification and financial information specific to consumers.</remarks>
    public class Consumer : Customer, IBusinessAssociate
    {
        public int ConsumerId => CustomerId;
        public int Id => ConsumerId;
        public double TotalAmount { get; set; }
    }
    /// <summary>
    /// Represents a business vendor associated with the system, providing vendor-specific information and
    /// functionality.
    /// </summary>
    /// <remarks>The Vendor class extends Customer and implements IBusinessAssociate, allowing it to be used
    /// in contexts where either a customer or a business associate is required. VendorId and Id both reference the
    /// underlying CustomerId, ensuring consistent identification across related types.</remarks>
    public class Vendor : Customer, IBusinessAssociate
    {
        public int VendorId => CustomerId;
        public int Id => VendorId;
        public double TotalAmount { get; set; }
    }
}
