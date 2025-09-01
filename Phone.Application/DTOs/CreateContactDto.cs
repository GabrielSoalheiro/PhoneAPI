using System.ComponentModel.DataAnnotations;

namespace Phone.Application.DTOs
{
    public class CreateContactDto
    {
        [Required(ErrorMessage = "Nome é obrigatório")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Nome deve ter entre 2 e 100 caracteres")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "Telefone é obrigatório")]
        [RegularExpression(@"^\(\d{2}\)\s\d{4,5}-\d{4}$", ErrorMessage = "Telefone deve estar no formato (99) 99999-9999")]
        public string Telefone { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email deve ter um formato válido")]
        public string Email { get; set; } = string.Empty;

        public DateTime? DataNascimento { get; set; }

        [Required(ErrorMessage = "Pelo menos um endereço é obrigatório")]
        [MinLength(1, ErrorMessage = "Pelo menos um endereço é obrigatório")]
        public List<CreateAddressDto> Enderecos { get; set; } = new();
    }
}
