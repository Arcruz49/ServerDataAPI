# ServerDataAPI

This API provides basic server monitoring on Linux, including **server status**, **CPU temperature**, and **RAM usage**.

## Base URL

```
http://<your-server>:5000/api/sensors
```

## API Routes

### 1. CheckStatus
- **Route:** `/CheckStatus`  
- **Method:** GET  
- **Description:** Checks if the server is online.  
- **Example response:**  
```json
"Server Online"
```

### 2. GetTemperature
- **Route:** `/GetTemperature`  
- **Method:** GET  
- **Description:** Returns the current CPU temperature in Celsius.  
- **Example response:**  
```json
{
  "temperature": 45.5
}
```

### 3. RamUsage
- **Route:** `/RamUsage`  
- **Method:** GET  
- **Description:** Returns the server RAM usage, including total RAM, used RAM, free RAM, and percentage used.  
- **Example response:**  
```json
{
  "totalMB": 16000,
  "usedMB": 8000,
  "freeMB": 8000,
  "usedPercent": 50.0
}
```
