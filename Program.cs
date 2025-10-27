using System.Net.Sockets;
using System.Text;

const string program = @"
def f():
  global RPC = rpc_factory(""xmlrpc"", ""⁦http://localhost:41414⁩"")
  # Remote procedure call object for the OnRobot gripper.
  # Using RPC, we can call functions inside the gripper's control system.

  global TOOL_INDEX = 0
  # One robot can have many grippers. We have only 1.
  # This constant needed by OnRobot RPC functions.

  def rg_is_busy():
    return RPC.rg_get_busy(TOOL_INDEX)
  end

  # width: [0, 110]
  # force: [0, 40]
  def rg_grip(width, force = 10):
    RPC.rg_grip(TOOL_INDEX, width + .0, force + .0)
    # + .0 required, because rg_grip expects float

    sleep(0.01) # Wait for the gripper to start moving before checking rg_is_busy()
    while (rg_is_busy()):
    end
  end

  # sequence start
  p1 = p[-0.131, -.295, .473, 2.5, 0.0, 0.030]
  p2 = p[.01989, -.4783, -.13, 3.14, 0, 0]
  p3 = p[.01989, -.4783, -.010, 3.14, 0, 0]
  p4 = p[-.1248, -.2864, -.010, 3.14, 0, 0]
  p5 = p[-.1248, -.2864, -.095, 3.14, 0, 0]
  p6 = p[-.1248, -.2864, -.010, 3.14, 0, 0]
  p7 = p[-0.12427, -.4836, -.010, 3.14, 0, 0]
  p8 = p[-0.12427, -.4836, -.13, 3.14, 0, 0]
  p9 = p[-0.12427, -.4836, -.010, 3.14, 0, 0]
  p10 = p[-.326, -.473, -.010, 3.14, 0, 0]
  p11 = p[-.326, -.473, -.13, 3.14, 0, 0]
  times = 0
  while (times < 1):
    movej(get_inverse_kin(p1))
    rg_grip(50)
    movej(get_inverse_kin(p2))
    rg_grip(17)
    movel(get_inverse_kin(p3))
    movel(get_inverse_kin(p4))
    movel(get_inverse_kin(p5))
    rg_grip(50)
    movel(get_inverse_kin(p6))
    movel(get_inverse_kin(p7))
    rg_grip(17)
    movej(get_inverse_kin(p8))
    movel(get_inverse_kin(p9))
    movel(get_inverse_kin(p4))
    movel(get_inverse_kin(p5))
    rg_grip(50)
    movel(get_inverse_kin(p4))
    movel(get_inverse_kin(p10))
    movej(get_inverse_kin(p11))
    rg_grip(17)
    movel(get_inverse_kin(p10))
    movel(get_inverse_kin(p4))
    movel(get_inverse_kin(p5))
    rg_grip(50)
    movel(get_inverse_kin(p4))
    times = times + 1
  end
end
";

const int urscriptPort = 30002, dashboardPort = 29999;
const string IpAddress = "172.17.0.2";

void SendString(string host, int port, string message)
{
    using var client = new TcpClient(host, port);
    using var stream = client.GetStream();
    stream.Write(Encoding.ASCII.GetBytes(message));
}

SendString(IpAddress, dashboardPort, "brake release\n");
SendString(IpAddress, urscriptPort, program);
// To stop:
// SendString(IpAddress, dashboardPort, "stop\n");