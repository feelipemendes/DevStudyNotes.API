namespace DevStudyNotes.API.Entities
{
    public class StudyNote
    {
        public StudyNote(string? title, string? description, bool isPublic)
        {
            Title = title;
            Description = description;
            IsPublic = isPublic;

            StudyNoteReactions = new List<StudyNoteReaction>();
            CreatedAt = DateTime.Now;
        }

        public int Id { get; private set; }
        public string? Title { get; private set; }
        public string? Description { get; private set; }
        public bool IsPublic { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public List<StudyNoteReaction> StudyNoteReactions { get; private set; }

        public void AddReaction(bool isPositive)
        {
            if (!isPositive)
            {
                throw new InvalidOperationException();
            }

            StudyNoteReactions.Add(new StudyNoteReaction(isPositive));
        }
    }
}
