namespace SistemaVenta.API.Utilidad
{

    //respuesta a las solicitudes de las apis
    public class Response<T>
    {

        public bool status {  get; set; }
        public T value { get; set; }

        public string msg { get; set; }

    }
}
