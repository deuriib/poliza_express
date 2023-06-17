using MongoFramework;
using MongoFramework.Attributes;

namespace PolizaExpress.SharedKernel.ValueObjects;

public record Vehiculo(
    string Placa,
    string Modelo);