using System;

public class RemoteControlCar
{
    int _batteryPercentage = 100;
    int _distanceDrivenInMeters;
    string[] _sponsors = Array.Empty<string>();
    int _latestSerialNum;

    public void Drive()
    {
        if (_batteryPercentage <= 0) return;
        _batteryPercentage -= 10;
        _distanceDrivenInMeters += 2;
    }

    public void SetSponsors(params string[] sponsors) => _sponsors = sponsors;

    public string DisplaySponsor(int sponsorNum) => _sponsors[sponsorNum];

    public bool GetTelemetryData(ref int serialNum,
        out int batteryPercentage, out int distanceDrivenInMeters)
    {
        batteryPercentage = _batteryPercentage;
        distanceDrivenInMeters = _distanceDrivenInMeters;
        
        if (serialNum < _latestSerialNum)
        {
            serialNum = _latestSerialNum;
            batteryPercentage = -1;
            distanceDrivenInMeters = -1;
            return false;
        }
        _latestSerialNum = serialNum;
        return true;
    }

    public static RemoteControlCar Buy()
    {
        return new RemoteControlCar();
    }
}

public class TelemetryClient
{
    readonly RemoteControlCar _car;

    public TelemetryClient(RemoteControlCar car)
    {
        _car = car;
    }

    public string GetBatteryUsagePerMeter(int serialNum)
    {
        if (_car.GetTelemetryData(ref serialNum, out var batteryPercentage, out var distanceDrivenInMeters) && distanceDrivenInMeters > 0)
        {
            return $"usage-per-meter={(100 - batteryPercentage) / distanceDrivenInMeters}";
        }

        return "no data";
    }
}
