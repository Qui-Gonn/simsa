namespace Tkd.Simsa.Domain.EventManagement;

/// <summary>
/// Value object for documentation notes in examination results.
/// </summary>
public record ExaminationNotes
{
    /// <summary>
    /// Gets the note content.
    /// </summary>
    public string Content { get; }
    
    /// <summary>
    /// Gets the timestamp when notes were created.
    /// </summary>
    public DateTime CreatedAt { get; }
    
    /// <summary>
    /// Gets the maximum allowed length for notes.
    /// </summary>
    public const int MaxLength = 2000;
    
    /// <summary>
    /// Empty notes instance.
    /// </summary>
    public static ExaminationNotes Empty => new(string.Empty);
    
    private ExaminationNotes(string content, DateTime? createdAt = null)
    {
        Content = content ?? string.Empty;
        CreatedAt = createdAt ?? DateTime.UtcNow;
    }
    
    /// <summary>
    /// Creates examination notes with validation.
    /// </summary>
    /// <param name="content">The note content</param>
    /// <returns>A validated notes instance</returns>
    /// <exception cref="ArgumentException">Thrown when content exceeds maximum length</exception>
    public static ExaminationNotes Create(string content)
    {
        if (string.IsNullOrEmpty(content))
            return Empty;
            
        if (content.Length > MaxLength)
            throw new ArgumentException($"Notes content cannot exceed {MaxLength} characters");
            
        return new ExaminationNotes(content.Trim());
    }
    
    /// <summary>
    /// Gets whether the notes are empty.
    /// </summary>
    public bool IsEmpty => string.IsNullOrWhiteSpace(Content);
    
    /// <summary>
    /// Gets the content length.
    /// </summary>
    public int Length => Content.Length;
    
    /// <summary>
    /// Appends additional content to existing notes.
    /// </summary>
    /// <param name="additionalContent">Content to append</param>
    /// <returns>Updated notes with appended content</returns>
    public ExaminationNotes Append(string additionalContent)
    {
        if (string.IsNullOrWhiteSpace(additionalContent))
            return this;
            
        var newContent = IsEmpty 
            ? additionalContent.Trim()
            : $"{Content}\n{additionalContent.Trim()}";
            
        return Create(newContent);
    }
    
    public override string ToString() => Content;
}
