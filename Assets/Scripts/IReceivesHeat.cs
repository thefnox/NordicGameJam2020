public interface IReceivesHeat
{
    float getThermalConductivity();
    float getTemperature();
    void receiveHeat(float value, float distance);
    void emitHeat();
}
