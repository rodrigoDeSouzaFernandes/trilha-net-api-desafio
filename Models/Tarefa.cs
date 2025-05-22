using System.ComponentModel.DataAnnotations;

namespace TrilhaApiDesafio.Models;

public class Tarefa
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O título é obrigatório")]
    [StringLength(
        50,
        MinimumLength = 3,
        ErrorMessage = "O título deve ter entre 3 e 50 caracteres."
    )]
    public string Titulo { get; set; }

    [MaxLength(200, ErrorMessage = "A descrição deve ter no máximo 200 caracteres.")]
    public string Descricao { get; set; }

    [Required(ErrorMessage = "A data é obrigatória")]
    public DateTime? Data { get; set; }

    [Required(ErrorMessage = "O status é obrigatório.")]
    public EnumStatusTarefa Status { get; set; }
}
