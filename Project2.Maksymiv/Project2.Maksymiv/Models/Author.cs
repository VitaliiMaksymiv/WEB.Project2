namespace Librairie.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents the Author.
    /// </summary>
    public class Author
    {
        /// <summary>
        /// Gets or sets the Id of the <see cref="Author"/>.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the FirstName of the <see cref="Author"/>.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the LastName of the <see cref="Author"/>.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the relations between <see cref="Author"/> and <see cref="Book"/>.
        /// </summary>
        public virtual List<AuthorBook> AuthorBooks { get; set; }
    }
}