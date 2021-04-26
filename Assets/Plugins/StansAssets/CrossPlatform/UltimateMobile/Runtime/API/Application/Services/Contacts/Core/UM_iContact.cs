namespace SA.CrossPlatform.App
{
    /// <summary>
    /// Phone contact record model.
    /// </summary>
    public interface UM_iContact
    {
        /// <summary>
        /// Contact name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Phone number
        /// </summary>
        string Phone { get; }

        /// <summary>
        /// The email address
        /// </summary>
        string Email { get; }

        
        /// <summary>
        /// The name of the organization associated with the contact.
        /// </summary>
        string OrganizationName { get; }

        /// <summary>
        /// The contact’s job title.
        /// </summary>
        string JobTitle { get; }
        
        /// <summary>
        /// A note associated with a contact.
        /// </summary>
        string Note { get; }
    }
}
