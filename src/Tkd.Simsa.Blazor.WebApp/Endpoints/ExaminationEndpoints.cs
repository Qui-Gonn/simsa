using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tkd.Simsa.Application.EventManagement;

namespace Tkd.Simsa.Blazor.WebApp.Endpoints;

/// <summary>
/// API endpoints for examination-specific operations.
/// </summary>
public static class ExaminationEndpoints
{
    /// <summary>
    /// Maps examination-specific endpoints to the route builder.
    /// </summary>
    /// <param name="app">The route builder</param>
    /// <returns>The route builder for chaining</returns>
    public static IEndpointRouteBuilder MapExaminationEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/examination")
                       .WithTags("Examination");

        // Query endpoints
        group.MapGet("/{examinationId:guid}", GetExaminationById)
             .WithName("GetExaminationById")
             .WithSummary("Get examination details by ID");

        group.MapGet("/{examinationId:guid}/participants", GetExaminationParticipants)
             .WithName("GetExaminationParticipants")
             .WithSummary("Get participants for an examination");

        group.MapGet("/{examinationId:guid}/progress", GetExaminationProgress)
             .WithName("GetExaminationProgress")
             .WithSummary("Get examination progress");

        group.MapGet("/{examinationId:guid}/results", GetExaminationResults)
             .WithName("GetExaminationResults")
             .WithSummary("Get all examination results");

        group.MapGet("/{examinationId:guid}/participants/{participantId:guid}/results", GetParticipantResults)
             .WithName("GetParticipantResults")
             .WithSummary("Get examination results for a specific participant");

        // Command endpoints
        group.MapPut("/{examinationId:guid}/participants/{participantId:guid}/discipline-result", UpdateDisciplineResult)
             .WithName("UpdateDisciplineResult")
             .WithSummary("Update discipline result for a participant");

        group.MapPost("/{examinationId:guid}/current-discipline", SetCurrentDiscipline)
             .WithName("SetCurrentDiscipline")
             .WithSummary("Set the current discipline for the examination");

        group.MapPost("/{examinationId:guid}/participants/{participantId:guid}/notes", AddExaminationNotes)
             .WithName("AddExaminationNotes")
             .WithSummary("Add notes for a participant's examination");

        group.MapPost("/{examinationId:guid}/participants/{participantId:guid}/complete", CompleteParticipantExamination)
             .WithName("CompleteParticipantExamination")
             .WithSummary("Mark a participant's examination as complete");

        group.MapPost("/{examinationId:guid}/discipline/{disciplineType}/complete", CompleteDiscipline)
             .WithName("CompleteDiscipline")
             .WithSummary("Mark a discipline as complete for the examination");

        return app;
    }

    private static async Task<IResult> GetExaminationById(
        Guid examinationId,
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken)
    {
        try
        {
            var query = new GetExaminationByIdQuery(examinationId);
            var result = await mediator.Send(query, cancellationToken);
            
            return result != null ? Results.Ok(result) : Results.NotFound();
        }
        catch (Exception ex)
        {
            return Results.Problem($"Error retrieving examination: {ex.Message}");
        }
    }

    private static async Task<IResult> GetExaminationParticipants(
        Guid examinationId,
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken)
    {
        try
        {
            var query = new GetExaminationParticipantsQuery(examinationId);
            var result = await mediator.Send(query, cancellationToken);
            
            return Results.Ok(result);
        }
        catch (Exception ex)
        {
            return Results.Problem($"Error retrieving examination participants: {ex.Message}");
        }
    }

    private static async Task<IResult> GetExaminationProgress(
        Guid examinationId,
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken)
    {
        try
        {
            var query = new GetExaminationProgressQuery(examinationId);
            var result = await mediator.Send(query, cancellationToken);
            
            return Results.Ok(result);
        }
        catch (Exception ex)
        {
            return Results.Problem($"Error retrieving examination progress: {ex.Message}");
        }
    }

    private static async Task<IResult> GetExaminationResults(
        Guid examinationId,
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken)
    {
        try
        {
            var query = new GetAllExaminationResultsQuery(examinationId);
            var result = await mediator.Send(query, cancellationToken);
            
            return Results.Ok(result);
        }
        catch (Exception ex)
        {
            return Results.Problem($"Error retrieving examination results: {ex.Message}");
        }
    }

    private static async Task<IResult> GetParticipantResults(
        Guid examinationId,
        Guid participantId,
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken)
    {
        try
        {
            var query = new GetExaminationResultsQuery(examinationId, participantId);
            var result = await mediator.Send(query, cancellationToken);
            
            return result != null ? Results.Ok(result) : Results.NotFound();
        }
        catch (Exception ex)
        {
            return Results.Problem($"Error retrieving participant results: {ex.Message}");
        }
    }

    private static async Task<IResult> UpdateDisciplineResult(
        Guid examinationId,
        Guid participantId,
        [FromBody] UpdateDisciplineResultRequest request,
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken)
    {
        try
        {
            // Convert DisciplineType to Discipline
            var discipline = new Domain.EventManagement.Discipline(request.DisciplineType, "", "", 0);
            var rating = Domain.EventManagement.DisciplineRating.FromNumeric(request.Result.Rating.NumericValue);
            var notes = Domain.EventManagement.ExaminationNotes.Create(request.Result.Notes);
            
            var command = new UpdateDisciplineResultCommand(
                examinationId,
                participantId,
                discipline,
                rating,
                notes,
                request.DocumentedBy ?? "API User");
            
            await mediator.Send(command, cancellationToken);
            
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.Problem($"Error updating discipline result: {ex.Message}");
        }
    }

    private static async Task<IResult> SetCurrentDiscipline(
        Guid examinationId,
        [FromBody] SetCurrentDisciplineRequest request,
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken)
    {
        try
        {
            var discipline = new Domain.EventManagement.Discipline(request.DisciplineType, "", "", 0);
            var command = new SetCurrentDisciplineCommand(examinationId, discipline);
            await mediator.Send(command, cancellationToken);
            
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.Problem($"Error setting current discipline: {ex.Message}");
        }
    }

    private static async Task<IResult> AddExaminationNotes(
        Guid examinationId,
        Guid participantId,
        [FromBody] AddExaminationNotesRequest request,
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken)
    {
        try
        {
            var discipline = new Domain.EventManagement.Discipline(request.DisciplineType, "", "", 0);
            var notes = Domain.EventManagement.ExaminationNotes.Create(request.Notes);
            
            var command = new AddExaminationNotesCommand(
                examinationId,
                participantId,
                discipline,
                notes,
                request.DocumentedBy ?? "API User");
            
            await mediator.Send(command, cancellationToken);
            
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.Problem($"Error adding examination notes: {ex.Message}");
        }
    }

    private static async Task<IResult> CompleteParticipantExamination(
        Guid examinationId,
        Guid participantId,
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken)
    {
        try
        {
            var command = new CompleteParticipantExaminationCommand(examinationId, participantId, "API User");
            await mediator.Send(command, cancellationToken);
            
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.Problem($"Error completing participant examination: {ex.Message}");
        }
    }

    private static async Task<IResult> CompleteDiscipline(
        Guid examinationId,
        Domain.EventManagement.DisciplineType disciplineType,
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken)
    {
        try
        {
            var discipline = new Domain.EventManagement.Discipline(disciplineType, "", "", 0);
            var command = new CompleteDisciplineCommand(examinationId, discipline, "API User");
            await mediator.Send(command, cancellationToken);
            
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.Problem($"Error completing discipline: {ex.Message}");
        }
    }
}

/// <summary>
/// Request models for examination API endpoints.
/// </summary>
public record UpdateDisciplineResultRequest(
    Domain.EventManagement.DisciplineType DisciplineType,
    DisciplineResultDto Result,
    string? DocumentedBy = null);

public record SetCurrentDisciplineRequest(Domain.EventManagement.DisciplineType DisciplineType);

public record AddExaminationNotesRequest(
    Domain.EventManagement.DisciplineType DisciplineType,
    string Notes,
    string? DocumentedBy = null);
