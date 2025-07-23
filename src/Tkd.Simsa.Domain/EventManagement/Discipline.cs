namespace Tkd.Simsa.Domain.EventManagement;

/// <summary>
/// Value object representing a single examination discipline with validation.
/// </summary>
public record Discipline(
    DisciplineType Type,
    string Name,
    string Description,
    int Order)
{
    /// <summary>
    /// Validates the discipline properties.
    /// </summary>
    /// <returns>Collection of validation errors, empty if valid.</returns>
    public IEnumerable<string> Validate()
    {
        if (string.IsNullOrWhiteSpace(Name))
            yield return "Discipline name cannot be empty";
            
        if (Name?.Length > 100)
            yield return "Discipline name cannot exceed 100 characters";
            
        if (string.IsNullOrWhiteSpace(Description))
            yield return "Discipline description cannot be empty";
            
        if (Description?.Length > 500)
            yield return "Discipline description cannot exceed 500 characters";
            
        if (Order < 0)
            yield return "Discipline order must be non-negative";
    }
    
    /// <summary>
    /// Creates a new discipline with validation.
    /// </summary>
    /// <param name="type">The discipline type</param>
    /// <param name="name">The discipline name</param>
    /// <param name="description">The discipline description</param>
    /// <param name="order">The order in examination sequence</param>
    /// <returns>A validated discipline instance</returns>
    /// <exception cref="ArgumentException">Thrown when validation fails</exception>
    public static Discipline Create(DisciplineType type, string name, string description, int order)
    {
        var discipline = new Discipline(type, name, description, order);
        var errors = discipline.Validate().ToList();
        
        if (errors.Any())
            throw new ArgumentException($"Invalid discipline: {string.Join(", ", errors)}");
            
        return discipline;
    }
}
