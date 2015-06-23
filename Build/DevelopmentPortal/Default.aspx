<%@ Page Language="vb" AutoEventWireup="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title>DPRA DEV Portal</title>
</head>

<body bgColor="#CACACA">

    <form id="form1" runat="server">

    <div>

        <center>

        <table border=2 width="830" bgColor="white" cellpadding="25" cellspacing="0">

            <tr>

                <td width="90%"><div style="font-family:Verdana, Geneva, Tahoma, sans-serif; font-size:larger; font-weight:bolder;">FDA Challenge: Development Portal</div></td>

                <td align="right" valign="middle">v0.905.1</td>

            </tr>

            <tr>

                <td colspan="2">

                     <p align="center">

                        <table cellpadding="15">
                            <tr>
                                <td bgcolor="#DEDEDE" width="830" colspan=2 align="center"><font face="verdana,tahoma"><b><a style="color:blue;" href="/ccnet">Cruise Control.NET</b></a> - Continuous Integration Monitoring Dashboard</font></td>
                            </tr>
                        </table>

                    </p>


        <% 
           
            Dim list As String() = System.IO.Directory.GetDirectories("C:\ccnet\buildartifacts\", "out.*")
            
            For Each item As String In list
                
                Dim sublist1 As String() = System.IO.Directory.GetDirectories(item, "*")

                For Each pubCheck As String In sublist1
                    
                    Dim sublist2 As String() = System.IO.Directory.GetDirectories(pubCheck, "*")
                                   
                    For Each appCheck As String In sublist2
                        
                        Dim sublist3 As String() = System.IO.Directory.GetDirectories(appCheck, "*")
                                   
                        For Each binCheck As String In sublist3
                    
                    
                            If binCheck.ToLower.EndsWith("bin") Then
                        
                                Dim binList As String() = System.IO.Directory.GetFiles(binCheck, "*")
                        
                                For Each bin As String In binList
                            
                                    If bin.ToLower.EndsWith("all.dll") Then
                            
                                        Dim fileVersion As String = System.Diagnostics.FileVersionInfo.GetVersionInfo(bin).FileVersion
                                        Dim productVersion As String = System.Diagnostics.FileVersionInfo.GetVersionInfo(bin).ProductVersion
                                        Dim assem As System.Reflection.Assembly = System.Reflection.Assembly.LoadFrom(bin)
                                        'Dim descAttr As System.Reflection.AssemblyDescriptionAttribute = System.Reflection.AssemblyDescriptionAttribute.GetCustomAttribute(assem, GetType(System.Reflection.AssemblyDescriptionAttribute))
                                      
                                
                                        Dim serviceSplit As List(Of String) = bin.Split("\").ToList()
                                        Dim serviceName As String = "Unidentified"
                                        
                                        Dim build As List(Of String) = item.Split(".").ToList()
                                        Dim buildId As String = build(build.Count - 1)
                                
                                        If serviceSplit.Count > 3 Then
                                            serviceName = serviceSplit(serviceSplit.Count - 3)
                                        End If
                                
            %>
        
                    <p align="center">

                        <table cellpadding="15">
                            <tr>
                                <td bgcolor="#DEDEDE" width="830" colspan=2 align="center"><font face="verdana,tahoma"><b><a style="color:blue;" href="/<%=buildId%>">/<%=buildId%></b></a> - [<%=serviceSplit(serviceSplit.Count - 1)%>]</font></td>
                            </tr>

                            <tr>
                                <td bgColor="#EFEFEF" width="50%" style="font-family:Courier New, Courier, monospace;">
    
                                    File Version: v<%=fileVersion%>
        
                                </td>
                                
                                <td bgColor="#EFEFEF" width="50%" style="font-family:Courier New, Courier, monospace;">
        
                                    Assembly Version: v<%=assem.GetName.Version%>

                                 </td>
    
                            </tr>
                        </table>

                    </p>
        
        <%
                                
                            End If
                            
                            
                        Next
                        
End If
        
Next
        
Next
                    
Next
                
Next
            
            
            %>

 
                    </td>
                </tr>
            </table>

            <p align="center"><font size="1" face="verdana">Copyright &copy;2008-2015 DPRA, Inc. &nbsp; All Rights Reserved.</font></p>

        </center>

    </div>

    </form>

</body>

</html>
