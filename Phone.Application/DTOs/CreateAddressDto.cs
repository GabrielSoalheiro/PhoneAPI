using System.ComponentModel.DataAnnotations;

namespace Phone.Application.DTOs
{
    public class CreateAddressDto
    {
        [Required(ErrorMessage = "Logradouro é obrigatório")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Logradouro deve ter entre 5 e 100 caracteres")]
        public string Logradouro { get; set; } = string.Empty;

        [Required(ErrorMessage = "Número é obrigatório")]
        public string Numero { get; set; } = string.Empty;

        public string? Complemento { get; set; }

        [Required(ErrorMessage = "Bairro é obrigatório")]
        public string Bairro { get; set; } = string.Empty;

        [Required(ErrorMessage = "Cidade é obrigatória")]
        public string Cidade { get; set; } = string.Empty;

        [Required(ErrorMessage = "Estado é obrigatório")]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "Estado deve ter 2 caracteres")]
        [RegularExpression(@"^[A-Z]{2}$", ErrorMessage = "Estado deve conter apenas letras maiúsculas")]
        public string Estado { get; set; } = string.Empty;

        [Required(ErrorMessage = "CEP é obrigatório")]
        [RegularExpression(@"^\d{5}-?\d{3}$", ErrorMessage = "CEP deve estar no formato 99999-999")]
        public string CEP { get; set; } = string.Empty;
    }
}
