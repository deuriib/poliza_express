using MongoFramework;
using MongoFramework.Attributes;

namespace PolizaExpress.SharedKernel.ValueObjects;

public record Cliente(
    string Nombre,
    string Identificacion,
    DateTime FechaNacimiento,
    string Ciudad,
    string Direccion
);