using System.Runtime.Serialization;
using PolizaExpress.SharedKernel.ValueObjects;

namespace PolizaExpress.SharedKernel.Tests
{
    public class ClienteTests
    {
        [Fact]
        public void Crear_client_con_valores_validos()
        {
            // Arrange
            string nombre = "John Doe";
            string identificacion = "123456789";
            DateTime fechaNacimiento = new DateTime(1990, 1, 1);
            string ciudad = "Ciudad";
            string direccion = "Dirección";

            // Act
            var cliente = new Cliente(nombre, identificacion, fechaNacimiento, ciudad, direccion);

            // Assert
            Assert.Equal(nombre, cliente.Nombre);
            Assert.Equal(identificacion, cliente.Identificacion);
            Assert.Equal(fechaNacimiento, cliente.FechaNacimiento);
            Assert.Equal(ciudad, cliente.Ciudad);
            Assert.Equal(direccion, cliente.Direccion);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Cliente_con_Nombre_vacio_o_nulo_ThrowsArgumentNullException(string? nombre)
        {
            // Arrange
            string identificacion = "123456789";
            DateTime fechaNacimiento = new DateTime(1990, 1, 1);
            string ciudad = "Ciudad";
            string direccion = "Dirección";

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
                new Cliente(nombre, identificacion, fechaNacimiento, ciudad, direccion));
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Cliente_con_Identificacion_vacio_o_nulo_ThrowsArgumentNullException(string? identificacion)
        {
            // Arrange
            string nombre = "John Doe";
            DateTime fechaNacimiento = new DateTime(1990, 1, 1);
            string ciudad = "Ciudad";
            string direccion = "Dirección";

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
                new Cliente(nombre, identificacion, fechaNacimiento, ciudad, direccion));
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Cliente_con_Ciudad_vacia_o_nula_ThrowsArgumentNullException(string? ciudad)
        {
            // Arrange
            string nombre = "John Doe";
            string identificacion = "123456789";
            DateTime fechaNacimiento = new DateTime(1990, 1, 1);
            string direccion = "Dirección";

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
                new Cliente(nombre, identificacion, fechaNacimiento, ciudad, direccion));
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Cliente_co_Direccion__vacia_o_nula_ThrowsArgumentNullException(string? direccion)
        {
            // Arrange
            string nombre = "John Doe";
            string identificacion = "123456789";
            DateTime fechaNacimiento = new DateTime(1990, 1, 1);
            string ciudad = "Ciudad";

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
                new Cliente(nombre, identificacion, fechaNacimiento, ciudad, direccion));
        }

        [Fact]
        public void Cliente_siendo_menor_de_edad_ThrowsInvalidOperationException()
        {
            // Arrange
            string nombre = "John Doe";
            string identificacion = "123456789";
            DateTime fechaNacimiento = new DateTime(2020, 1, 1);
            string ciudad = "Ciudad";
            string direccion = "Dirección";

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() =>
                new Cliente(nombre, identificacion, fechaNacimiento, ciudad, direccion));
        }
    }
}