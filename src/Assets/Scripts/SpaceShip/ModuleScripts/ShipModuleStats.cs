
public class ShipModuleStats
{
    public int BaseDurability { get; set; } = 100;
    public int CurrentDurability { get; set; }
    public int CurrentSpeed { get; set; } = 0;
    public int CurrentEnergyConsumptionPerBattle { get; set; } = 0;
    public int CurrentEnergyConsumptionPerDistance { get; set; } = 0;
    public int CurrentEnergyGenerationPerDistance { get; set; } = 0;
    public int CurrentEnergyConversionRate { get; set; } = 1;
    public int CurrentEnergyConsumptionPerCollection { get; set; } = 0;
    public int CurrentEnergyConsumptionPerRepair { get; set; } = 0;
    public int CurrentEnergyComsumptionPerShot { get; set; } = 0;
    public int CurrentRepairRate { get; set; } = 0;
    public int CurrentOreStorageCapacity { get; set; } = 0;
    public int CurrentEnergyCapacity { get; set; } = 0;
    public int CurrentOreCollectionRate { get; set; } = 0;
    public int CurrentDamage { get; set; } = 0;
    public int CurrentMaxHull { get; set; } = 0;
    public int CurrentAcceleration { get; set; } = 0;
}
