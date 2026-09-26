namespace Classification.Domain.Enttities
{
    public sealed class ClassificationCategory
    {
        public Guid Id { get; private set; }

        public string Code { get; private set; } = null!;

        public string Name { get; private set; } = null!;

        public Guid? ParentId { get; private set; }

        public int Level { get; private set; }

        public bool IsActive { get; private set; }

        public ClassificationCategory? Parent { get; private set; }

        public ICollection<ClassificationCategory> Children
        { get; private set; }
            = new List<ClassificationCategory>();

        private ClassificationCategory()
        {
        }

        public ClassificationCategory(
            string code,
            string name,
            int level,
            Guid? parentId = null)
        {
            Id = Guid.NewGuid();

            Code = code;
            Name = name;

            Level = level;

            ParentId = parentId;

            IsActive = true;
        }
    }
}
