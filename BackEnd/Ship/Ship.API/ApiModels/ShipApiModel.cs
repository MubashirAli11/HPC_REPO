namespace Ship.API.ApiModels
{
    public class ShipApiModel: BaseApiModel
    {
        public string Name { get; private set; }
        public double Length { get; private set; }
        public double Width { get; private set; }
        public string Code { get; private set; }
    }
}
