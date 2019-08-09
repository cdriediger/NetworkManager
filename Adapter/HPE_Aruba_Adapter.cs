using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using RestSharp;



namespace NetworkManager.Adapters
{
    public class HPE_Aruba_Adapter
    {
        private ArubaapiHelper api;

        public HPE_Aruba_Adapter(String ipAddress, String username, String password)
        {
            Console.WriteLine(String.Format("Creating new HPE_Aruba_Adapater. IpAddress: {0}, Username: {1}, Password {2}", ipAddress, username, password));
            api = new ArubaapiHelper(ipAddress, username, password);
            var connected = api.Connect();
            Console.WriteLine(String.Format("Connected?: {0}", connected));
        }

        public void Close()
        {
            api.Close();
        }

        public Boolean isConnected()
        {
            return api.connected;
        }

        public Dictionary<int, string> GetVlans()
        {
            var vlanList = new Dictionary<int, string>();
            var response = api.Query($"/rest/v3/vlans");
            var vlans = JsonConvert.DeserializeObject<Vlans>(response);
            foreach (var vlan in vlans.vlan_element)
            {
                vlanList.Add(vlan.vlan_id, vlan.name);
            }
            return vlanList;
        }

        public String GetPortState(int portId)
        {
            var response = api.Query($"/rest/v3/ports/{portId}");
            var state = JsonConvert.DeserializeObject<Port>(response).is_port_up;
            Console.WriteLine(response);
            if (state)
            {
                return "Up";
            }
            else
            {
                return "Down";
            }
        }
        
        public int GetPortVlan(int portId)
        {
            var response = api.Query($"/rest/v3/vlans-ports");
            var vlan_port_elements = JsonConvert.DeserializeObject<VlansPorts>(response).vlan_port_element;
            foreach (var vlanPort in vlan_port_elements)
            {
                if (vlanPort.port_id == portId.ToString())
                {
                    return vlanPort.vlan_id;
                }
            }
            return 0;
        }

        public void SetPortVlan(int portId, int vlanId)
        {
            var url = $"/rest/v3/vlans-ports";
            var port = new PortVlan(portId, vlanId);
            api.Post(url, port);
        }

        public string GetPortDescription(int portId)
        {
            var response = api.Query($"/rest/v3/ports/{portId}");
            return JsonConvert.DeserializeObject<Port>(response).name;
        }

        public void SetPortDescription(int portId, string description)
        {
            var url = $"/rest/v3/ports/{portId}";
            var response = api.Query(url);
            var port = JsonConvert.DeserializeObject<Port>(response);
            port.name = description;
            api.Put(url, port);
        }

        public String GetSystemName()
        {
            var response = api.Query("/rest/v3/system");
            var Name = JsonConvert.DeserializeObject<System>(response).name;
            return Name;
        }

        public void SetSystemName(string name)
        {
            var url = $"/rest/v3/system";
            api.Put(url, new SystemName(name));
        }

        public String GetLocation()
        {
            var response = api.Query("/rest/v3/system");
            var location = JsonConvert.DeserializeObject<System>(response).location;
            return location;
        }

        public void SetLocation(string location)
        {
            var url = $"/rest/v3/system";
            api.Put(url, new Location(location));
        }

        public String GetModelName()
        {
            var response = api.Query("/rest/v3/system/status");
            var Name = JsonConvert.DeserializeObject<SystemStatusSwitch>(response).product_model;
            return Name;
        }

        public Dictionary<String, String> GetSystemInfo()
        {
            var SystemInfo = new Dictionary<string, string>();

            var response = api.Query("/rest/v3/system");
            var System = JsonConvert.DeserializeObject<System>(response);
            SystemInfo["name"] = System.name;
            SystemInfo["location"] = System.location;

            response = api.Query("/rest/v3/system/status/switch");
            var SystemStatusSwitch = JsonConvert.DeserializeObject<Dictionary<String, object>>(response);
            SystemInfo["model"] = (String)SystemStatusSwitch["product_name"];

            response = api.Query("/rest/v3/ports");
            var Ports = JsonConvert.DeserializeObject<Dictionary<String, object>>(response);
            Console.WriteLine(Ports["collection_result"].GetType().ToString());
            var collection_result = (Newtonsoft.Json.Linq.JObject)Ports["collection_result"];
            SystemInfo["ports"] = collection_result["total_elements_count"].ToString();

            return SystemInfo;
        }

        public class System
        {
            public string uri { get; set; }
            public string name { get; set; }
            public string location { get; set; }
            public string contact { get; set; }
            public string device_operation_mode { get; set; }
            public Dictionary<string, string> default_gateway { get; set; }
        }

        public class SystemStatusSwitch
        {
            public string uri { get; set; }
            public string name { get; set; }
            public string serial_number { get; set; }
            public string firmware_version { get; set; }
            public string hardware_revision { get; set; }
            public string product_model { get; set; }
            public Dictionary<string, string> base_ethernet_address { get; set; }
            public int total_memory_in_bytes { get; set; }
            public int total_poe_consumption { get; set; }
        }

        public class Port
        {
            public string uri { get; set; }
            public string id { get; set; }
            public string name { get; set; }
            public Boolean is_port_enabled { get; set; }
            public Boolean is_port_up { get; set; }
            public string config_mode { get; set; }
            public string trunk_mode { get; set; }
            public string lacp_status { get; set; }
            public string trunk_group { get; set; }
            public Boolean is_flow_control_enabled { get; set; }
            public Boolean is_dsnoop_port_trusted { get; set; }
        }

        public class VlansPorts
        {
            public Dictionary<string, int> collection_result { get; set; }
            public List<VlanPortElement> vlan_port_element { get; set; }
        }

        public class VlanPortElement
        {
            public string uri { get; set; }
            public int vlan_id { get; set; }
            public string port_id { get; set; }
            public string port_mode { get; set; }
        }

        public class Vlans
        {
            public Dictionary<string, int> collection_result { get; set; }
            public List<vlan_element> vlan_element { get; set; }
        }

        public class vlan_element
        {
            public string uri { get; set; }
            public int vlan_id { get; set; }
            public string name { get; set; }
            public string status { get; set; }
            public string type { get; set; }
            public Boolean is_voice_enabled { get; set; }
            public Boolean is_jumbo_enabled { get; set; }
            public Boolean is_dsnoop_enabled { get; set; }
        }

        public class PortVlan
        {
            public int vlan_id { get; set; }
            public string port_id { get; set; }
            public string port_mode { get; set; }
            
            public PortVlan(int portId, int vlanId)
            {
                vlan_id = vlanId;
                port_id = portId.ToString();
                port_mode = "POM_UNTAGGED";
            }
        }

        public class SystemName
        {
            public string name {get; set;}

            public SystemName(string name)
            {
                this.name = name;
            }
        }

        public class Location
        {
            public string location {get; set;}

            public Location(string location)
            {
                this.location = location;
            }
        }

    }

    public class ArubaapiHelper
    {
        private string ipAddress;
        private string username;
        private string password;
        private string loginToken;
        private RestClient restClient;
        private DateTime reconnectTime;
        public Boolean connected;

        public ArubaapiHelper(String ipAddress, String username, String password)
        {
            this.ipAddress = ipAddress;
            this.username = username;
            this.password = password;
            this.connected = false;
            System.Console.CancelKeyPress += (s,e) => { Close(); };
        }

        public void Close()
        {
            if (connected)
            {
                Console.WriteLine("Closing Aruba api Helper...");
                var url = "/rest/v3/login-sessions";
                var request = new RestRequest(url, Method.DELETE);
                request.AddCookie("sessionId", loginToken);
                connected = false;
            }
        }

        public Boolean Connect()
        {
            var url = $"http://{ipAddress}";
            restClient = new RestClient(url);
            var loginrequest = new RestRequest("/rest/v1/login-sessions", Method.POST);
            var loginJson = String.Format("{0}\"userName\":\"{1}\", \"password\":\"{2}\"{3}", "{", username, password, "}");
            Console.WriteLine($"Login: JSON Data: {loginJson}");
            loginrequest.AddParameter("application/json", loginJson, ParameterType.RequestBody);

            IRestResponse response = restClient.Execute(loginrequest);
            Console.WriteLine(String.Format("Token: {0}", response.Content));
            if (response.Content.StartsWith("<HTML>"))
            {
                return false;
            }
            var returnValue = JsonConvert.DeserializeObject<Dictionary<string, string>>(response.Content);
            if (returnValue.ContainsKey("message"))
            {
                return false;
            }
            else
            {
                loginToken = JsonConvert.DeserializeObject<Dictionary<string, string>>(response.Content)["cookie"].Split('=')[1];
                var timeoutrequest = new RestRequest("/rest/v1/session-idle-timeout", Method.GET);
                timeoutrequest.AddCookie("sessionId", loginToken);
                IRestResponse timeoutresponse = restClient.Execute(timeoutrequest);
                var timeout = JsonConvert.DeserializeObject<Dictionary<string, string>>(timeoutresponse.Content)["timeout"];
                Console.WriteLine($"Timeout: {timeout}");
                reconnectTime = DateTime.Now.AddSeconds(Convert.ToDouble(timeout));
                connected = true;
                return true;
            }
            
        }

        private void ReconnectIfTimeouted()
        {
            Console.WriteLine($"Reconnect? {DateTime.Compare(DateTime.Now, reconnectTime)}");
            if (DateTime.Compare(DateTime.Now, reconnectTime) >= 0)
            {
                Console.WriteLine("Reconnecting...");
                Connect();
            }

        }

        public string Query(string url)
        {
            Console.WriteLine($"Query: GET {url}");
            //ReconnectIfTimeouted();
            var request = new RestRequest(url, Method.GET);
            request.AddCookie("sessionId", loginToken);

            IRestResponse response = restClient.Execute(request);
            return response.Content;
        }

        public string Put(string url, object data)
        {
            return Send(url, data, Method.PUT);
        }

        public string Post(string url, object data)
        {
            return Send(url, data, Method.POST);
        }

        public string Send(string url, object data, RestSharp.Method method)
        {
            var jsonData = JsonConvert.SerializeObject(data);
            Console.WriteLine($"Send: {method.ToString()} {url} | {jsonData}");
            //ReconnectIfTimeouted();
            var request = new RestRequest(url, method);
            request.AddCookie("sessionId", loginToken);
            request.AddJsonBody(jsonData);

            IRestResponse response = restClient.Execute(request);
            Console.WriteLine($"Respone: {response.Content}");
            if (response.Content.StartsWith("<HTML>"))
            {
                return "{}";
            }
            var returnValue = JsonConvert.DeserializeObject<Dictionary<string, string>>(response.Content);
            if (returnValue.ContainsKey("message"))
            {
                return "{}";
            }
            else
            {
                return response.Content;
            }
        }
    }
           
}
