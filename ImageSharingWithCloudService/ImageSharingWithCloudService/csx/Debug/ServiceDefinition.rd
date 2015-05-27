<?xml version="1.0" encoding="utf-8"?>
<serviceModel xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" name="ImageSharingWithCloudService" generation="1" functional="0" release="0" Id="4cc9eac6-5c52-485a-8df1-df2feee248a1" dslVersion="1.2.0.0" xmlns="http://schemas.microsoft.com/dsltools/RDSM">
  <groups>
    <group name="ImageSharingWithCloudServiceGroup" generation="1" functional="0" release="0">
      <componentports>
        <inPort name="imagesharingwithcloudserviceWebRole:HttpIn" protocol="http">
          <inToChannel>
            <lBChannelMoniker name="/ImageSharingWithCloudService/ImageSharingWithCloudServiceGroup/LB:imagesharingwithcloudserviceWebRole:HttpIn" />
          </inToChannel>
        </inPort>
        <inPort name="imagesharingwithcloudserviceWebRole:HttpsIn" protocol="https">
          <inToChannel>
            <lBChannelMoniker name="/ImageSharingWithCloudService/ImageSharingWithCloudServiceGroup/LB:imagesharingwithcloudserviceWebRole:HttpsIn" />
          </inToChannel>
        </inPort>
        <inPort name="imagesharingwithcloudserviceWorkerRole:ExternalHTTP" protocol="http">
          <inToChannel>
            <lBChannelMoniker name="/ImageSharingWithCloudService/ImageSharingWithCloudServiceGroup/LB:imagesharingwithcloudserviceWorkerRole:ExternalHTTP" />
          </inToChannel>
        </inPort>
      </componentports>
      <settings>
        <aCS name="Certificate|imagesharingwithcloudserviceWebRole:CS526DEMON" defaultValue="">
          <maps>
            <mapMoniker name="/ImageSharingWithCloudService/ImageSharingWithCloudServiceGroup/MapCertificate|imagesharingwithcloudserviceWebRole:CS526DEMON" />
          </maps>
        </aCS>
        <aCS name="imagesharingwithcloudserviceWebRole:Microsoft.ServiceBus.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/ImageSharingWithCloudService/ImageSharingWithCloudServiceGroup/MapimagesharingwithcloudserviceWebRole:Microsoft.ServiceBus.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="imagesharingwithcloudserviceWebRole:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/ImageSharingWithCloudService/ImageSharingWithCloudServiceGroup/MapimagesharingwithcloudserviceWebRole:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="imagesharingwithcloudserviceWebRoleInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/ImageSharingWithCloudService/ImageSharingWithCloudServiceGroup/MapimagesharingwithcloudserviceWebRoleInstances" />
          </maps>
        </aCS>
        <aCS name="imagesharingwithcloudserviceWorkerRole:Microsoft.ServiceBus.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/ImageSharingWithCloudService/ImageSharingWithCloudServiceGroup/MapimagesharingwithcloudserviceWorkerRole:Microsoft.ServiceBus.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="imagesharingwithcloudserviceWorkerRole:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/ImageSharingWithCloudService/ImageSharingWithCloudServiceGroup/MapimagesharingwithcloudserviceWorkerRole:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="imagesharingwithcloudserviceWorkerRoleInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/ImageSharingWithCloudService/ImageSharingWithCloudServiceGroup/MapimagesharingwithcloudserviceWorkerRoleInstances" />
          </maps>
        </aCS>
      </settings>
      <channels>
        <lBChannel name="LB:imagesharingwithcloudserviceWebRole:HttpIn">
          <toPorts>
            <inPortMoniker name="/ImageSharingWithCloudService/ImageSharingWithCloudServiceGroup/imagesharingwithcloudserviceWebRole/HttpIn" />
          </toPorts>
        </lBChannel>
        <lBChannel name="LB:imagesharingwithcloudserviceWebRole:HttpsIn">
          <toPorts>
            <inPortMoniker name="/ImageSharingWithCloudService/ImageSharingWithCloudServiceGroup/imagesharingwithcloudserviceWebRole/HttpsIn" />
          </toPorts>
        </lBChannel>
        <lBChannel name="LB:imagesharingwithcloudserviceWorkerRole:ExternalHTTP">
          <toPorts>
            <inPortMoniker name="/ImageSharingWithCloudService/ImageSharingWithCloudServiceGroup/imagesharingwithcloudserviceWorkerRole/ExternalHTTP" />
          </toPorts>
        </lBChannel>
        <sFSwitchChannel name="SW:imagesharingwithcloudserviceWorkerRole:InternalTCP">
          <toPorts>
            <inPortMoniker name="/ImageSharingWithCloudService/ImageSharingWithCloudServiceGroup/imagesharingwithcloudserviceWorkerRole/InternalTCP" />
          </toPorts>
        </sFSwitchChannel>
      </channels>
      <maps>
        <map name="MapCertificate|imagesharingwithcloudserviceWebRole:CS526DEMON" kind="Identity">
          <certificate>
            <certificateMoniker name="/ImageSharingWithCloudService/ImageSharingWithCloudServiceGroup/imagesharingwithcloudserviceWebRole/CS526DEMON" />
          </certificate>
        </map>
        <map name="MapimagesharingwithcloudserviceWebRole:Microsoft.ServiceBus.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/ImageSharingWithCloudService/ImageSharingWithCloudServiceGroup/imagesharingwithcloudserviceWebRole/Microsoft.ServiceBus.ConnectionString" />
          </setting>
        </map>
        <map name="MapimagesharingwithcloudserviceWebRole:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/ImageSharingWithCloudService/ImageSharingWithCloudServiceGroup/imagesharingwithcloudserviceWebRole/Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </setting>
        </map>
        <map name="MapimagesharingwithcloudserviceWebRoleInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/ImageSharingWithCloudService/ImageSharingWithCloudServiceGroup/imagesharingwithcloudserviceWebRoleInstances" />
          </setting>
        </map>
        <map name="MapimagesharingwithcloudserviceWorkerRole:Microsoft.ServiceBus.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/ImageSharingWithCloudService/ImageSharingWithCloudServiceGroup/imagesharingwithcloudserviceWorkerRole/Microsoft.ServiceBus.ConnectionString" />
          </setting>
        </map>
        <map name="MapimagesharingwithcloudserviceWorkerRole:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/ImageSharingWithCloudService/ImageSharingWithCloudServiceGroup/imagesharingwithcloudserviceWorkerRole/Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </setting>
        </map>
        <map name="MapimagesharingwithcloudserviceWorkerRoleInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/ImageSharingWithCloudService/ImageSharingWithCloudServiceGroup/imagesharingwithcloudserviceWorkerRoleInstances" />
          </setting>
        </map>
      </maps>
      <components>
        <groupHascomponents>
          <role name="imagesharingwithcloudserviceWebRole" generation="1" functional="0" release="0" software="C:\Users\Xing\documents\visual studio 2012\Projects\ImageSharingWithCloudService\ImageSharingWithCloudService\csx\Debug\roles\imagesharingwithcloudserviceWebRole" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaIISHost.exe " memIndex="1792" hostingEnvironment="frontendadmin" hostingEnvironmentVersion="2">
            <componentports>
              <inPort name="HttpIn" protocol="http" portRanges="85" />
              <inPort name="HttpsIn" protocol="https" portRanges="443">
                <certificate>
                  <certificateMoniker name="/ImageSharingWithCloudService/ImageSharingWithCloudServiceGroup/imagesharingwithcloudserviceWebRole/CS526DEMON" />
                </certificate>
              </inPort>
              <outPort name="imagesharingwithcloudserviceWorkerRole:InternalTCP" protocol="tcp">
                <outToChannel>
                  <sFSwitchChannelMoniker name="/ImageSharingWithCloudService/ImageSharingWithCloudServiceGroup/SW:imagesharingwithcloudserviceWorkerRole:InternalTCP" />
                </outToChannel>
              </outPort>
            </componentports>
            <settings>
              <aCS name="Microsoft.ServiceBus.ConnectionString" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;imagesharingwithcloudserviceWebRole&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;imagesharingwithcloudserviceWebRole&quot;&gt;&lt;e name=&quot;HttpIn&quot; /&gt;&lt;e name=&quot;HttpsIn&quot; /&gt;&lt;/r&gt;&lt;r name=&quot;imagesharingwithcloudserviceWorkerRole&quot;&gt;&lt;e name=&quot;ExternalHTTP&quot; /&gt;&lt;e name=&quot;InternalTCP&quot; /&gt;&lt;/r&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
            <storedcertificates>
              <storedCertificate name="Stored0CS526DEMON" certificateStore="My" certificateLocation="System">
                <certificate>
                  <certificateMoniker name="/ImageSharingWithCloudService/ImageSharingWithCloudServiceGroup/imagesharingwithcloudserviceWebRole/CS526DEMON" />
                </certificate>
              </storedCertificate>
            </storedcertificates>
            <certificates>
              <certificate name="CS526DEMON" />
            </certificates>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/ImageSharingWithCloudService/ImageSharingWithCloudServiceGroup/imagesharingwithcloudserviceWebRoleInstances" />
            <sCSPolicyUpdateDomainMoniker name="/ImageSharingWithCloudService/ImageSharingWithCloudServiceGroup/imagesharingwithcloudserviceWebRoleUpgradeDomains" />
            <sCSPolicyFaultDomainMoniker name="/ImageSharingWithCloudService/ImageSharingWithCloudServiceGroup/imagesharingwithcloudserviceWebRoleFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
        <groupHascomponents>
          <role name="imagesharingwithcloudserviceWorkerRole" generation="1" functional="0" release="0" software="C:\Users\Xing\documents\visual studio 2012\Projects\ImageSharingWithCloudService\ImageSharingWithCloudService\csx\Debug\roles\imagesharingwithcloudserviceWorkerRole" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaWorkerHost.exe " memIndex="1792" hostingEnvironment="consoleroleadmin" hostingEnvironmentVersion="2">
            <componentports>
              <inPort name="ExternalHTTP" protocol="http" portRanges="10000" />
              <inPort name="InternalTCP" protocol="tcp" />
              <outPort name="imagesharingwithcloudserviceWorkerRole:InternalTCP" protocol="tcp">
                <outToChannel>
                  <sFSwitchChannelMoniker name="/ImageSharingWithCloudService/ImageSharingWithCloudServiceGroup/SW:imagesharingwithcloudserviceWorkerRole:InternalTCP" />
                </outToChannel>
              </outPort>
            </componentports>
            <settings>
              <aCS name="Microsoft.ServiceBus.ConnectionString" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;imagesharingwithcloudserviceWorkerRole&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;imagesharingwithcloudserviceWebRole&quot;&gt;&lt;e name=&quot;HttpIn&quot; /&gt;&lt;e name=&quot;HttpsIn&quot; /&gt;&lt;/r&gt;&lt;r name=&quot;imagesharingwithcloudserviceWorkerRole&quot;&gt;&lt;e name=&quot;ExternalHTTP&quot; /&gt;&lt;e name=&quot;InternalTCP&quot; /&gt;&lt;/r&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/ImageSharingWithCloudService/ImageSharingWithCloudServiceGroup/imagesharingwithcloudserviceWorkerRoleInstances" />
            <sCSPolicyUpdateDomainMoniker name="/ImageSharingWithCloudService/ImageSharingWithCloudServiceGroup/imagesharingwithcloudserviceWorkerRoleUpgradeDomains" />
            <sCSPolicyFaultDomainMoniker name="/ImageSharingWithCloudService/ImageSharingWithCloudServiceGroup/imagesharingwithcloudserviceWorkerRoleFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
      </components>
      <sCSPolicy>
        <sCSPolicyUpdateDomain name="imagesharingwithcloudserviceWebRoleUpgradeDomains" defaultPolicy="[5,5,5]" />
        <sCSPolicyUpdateDomain name="imagesharingwithcloudserviceWorkerRoleUpgradeDomains" defaultPolicy="[5,5,5]" />
        <sCSPolicyFaultDomain name="imagesharingwithcloudserviceWebRoleFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyFaultDomain name="imagesharingwithcloudserviceWorkerRoleFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyID name="imagesharingwithcloudserviceWebRoleInstances" defaultPolicy="[1,1,1]" />
        <sCSPolicyID name="imagesharingwithcloudserviceWorkerRoleInstances" defaultPolicy="[1,1,1]" />
      </sCSPolicy>
    </group>
  </groups>
  <implements>
    <implementation Id="bd85fc41-4ca7-45d3-8b6d-edc0fccc274f" ref="Microsoft.RedDog.Contract\ServiceContract\ImageSharingWithCloudServiceContract@ServiceDefinition">
      <interfacereferences>
        <interfaceReference Id="9befa60c-9d81-4601-bb77-7aeaae41586c" ref="Microsoft.RedDog.Contract\Interface\imagesharingwithcloudserviceWebRole:HttpIn@ServiceDefinition">
          <inPort>
            <inPortMoniker name="/ImageSharingWithCloudService/ImageSharingWithCloudServiceGroup/imagesharingwithcloudserviceWebRole:HttpIn" />
          </inPort>
        </interfaceReference>
        <interfaceReference Id="cac156b0-2fcb-4487-921f-23b2ae55adff" ref="Microsoft.RedDog.Contract\Interface\imagesharingwithcloudserviceWebRole:HttpsIn@ServiceDefinition">
          <inPort>
            <inPortMoniker name="/ImageSharingWithCloudService/ImageSharingWithCloudServiceGroup/imagesharingwithcloudserviceWebRole:HttpsIn" />
          </inPort>
        </interfaceReference>
        <interfaceReference Id="eaeceb93-78b2-4b61-a32c-54b1fa38efa2" ref="Microsoft.RedDog.Contract\Interface\imagesharingwithcloudserviceWorkerRole:ExternalHTTP@ServiceDefinition">
          <inPort>
            <inPortMoniker name="/ImageSharingWithCloudService/ImageSharingWithCloudServiceGroup/imagesharingwithcloudserviceWorkerRole:ExternalHTTP" />
          </inPort>
        </interfaceReference>
      </interfacereferences>
    </implementation>
  </implements>
</serviceModel>