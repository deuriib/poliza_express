namespace PolizaExpress.SharedKernel.Dtos;

public record ClienteDto(
    string Nombre, 
    string Identificacion, 
    DateTime FechaNacimiento, 
    string Ciudad, 
    string Direccion);