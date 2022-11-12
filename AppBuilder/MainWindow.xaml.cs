using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AppBuilder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string projectName = "";
        string projectNamespace;
        string projectInstanceName;
        string functionName;
        string functionNameReferenceName;
        string repositoryInterfaceName;
        string repositoryName;
        string repositoryInstanceName;
        string controllerName;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void openFileBTN_Click(object sender, RoutedEventArgs e)
        {
           OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                openFileTB.Text = openFileDialog.FileName;
                projectName = openFileDialog.SafeFileName.Substring(0, openFileDialog.SafeFileName.IndexOf('.'));

            }
        }

        private void generateBTN_Click(object sender, RoutedEventArgs e)
        {
            // Read Excel file
            ReadExcel();
        }

        private void ReadExcel()
        {
            List<string> columnNamesList = new List<string>();
            List<string> dataTypesList = new List<string>();
            List<string> controlTypeList = new List<string>();
            List<string> lookupNameList = new List<string>();
            List<string> referenceTableList = new List<string>();
            List<string> referenceColumnList = new List<string>();

            OleDbConnectionStringBuilder sbConnection = new OleDbConnectionStringBuilder();
            String strExtendedProperties = String.Empty;
            sbConnection.DataSource = openFileTB.Text;

            if (System.IO.Path.GetExtension(openFileTB.Text).Equals(".xls"))//for 97-03 Excel file
            {
                sbConnection.Provider = "Microsoft.Jet.OLEDB.4.0";
                strExtendedProperties = "Excel 8.0;HDR=Yes;IMEX=1";//HDR=ColumnHeader,IMEX=InterMixed
            }
            else if (System.IO.Path.GetExtension(openFileTB.Text).Equals(".xlsx"))  //for 2007 Excel file
            {
                sbConnection.Provider = "Microsoft.ACE.OLEDB.12.0";
                strExtendedProperties = "Excel 12.0;HDR=Yes;IMEX=1";
            }
            sbConnection.Add("Extended Properties", strExtendedProperties);

          
            using (OleDbConnection connection = new OleDbConnection(sbConnection.ToString()))
            {
                connection.Open();

                DataTable dataTable = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                var temp1 = dataTable.Rows[0]["Table_Name"];

                //OleDbCommand command = new OleDbCommand("select * from " + dataTable.Rows[0]["Table_Name"], connection);

                foreach (DataRow drSheet in dataTable.Rows)
                {
                    if (drSheet["TABLE_NAME"].ToString().Contains("$"))//checks whether row contains '_xlnm#_FilterDatabase' or sheet name(i.e. sheet name always ends with $ sign)
                    {
                        projectNamespace = this.namespaceTB.Text;
                        projectInstanceName = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(projectName.ToLower());

                        string sheetName = drSheet["TABLE_NAME"].ToString();
                        functionNameReferenceName = sheetName.Replace("$", "");
                        functionNameReferenceName = System.Text.Json.JsonNamingPolicy.CamelCase.ConvertName(functionNameReferenceName);
                        functionName = char.ToUpper(functionNameReferenceName[0]) + functionNameReferenceName.Substring(1);

                        repositoryInterfaceName = "I" + functionName + "Repository";
                        repositoryName = functionName + "Repository";
                        repositoryInstanceName = functionNameReferenceName + "Repository";
                        controllerName = functionName + "Controller";
                        
                        OleDbCommand command = new OleDbCommand("select * from [" + sheetName + "]", connection);
                        int counter = 0;
                        using (OleDbDataReader dr = command.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                if(counter == 0)
                                {
                                    DirectoryInfo directoryInfo = Directory.CreateDirectory(projectName);
                                    directoryInfo.CreateSubdirectory("DB");
                                    directoryInfo.CreateSubdirectory("API");
                                    directoryInfo.CreateSubdirectory("UI");

                                    directoryInfo.CreateSubdirectory("API\\Controllers");
                                    directoryInfo.CreateSubdirectory("API\\Model");
                                    directoryInfo.CreateSubdirectory("API\\Repository");

                                    directoryInfo.CreateSubdirectory("API\\Model\\Common");
                                    directoryInfo.CreateSubdirectory("API\\Model\\DAL");
                                    directoryInfo.CreateSubdirectory("API\\Model\\DropDown");
                                    directoryInfo.CreateSubdirectory("API\\Model\\DTO");

                                    directoryInfo.CreateSubdirectory("API\\Repository\\Implementation");
                                    directoryInfo.CreateSubdirectory("API\\Repository\\Interface");

                                    string inputHelpersFolderPath = "C:\\HelperFiles\\Template Files";

                                    string controllerFileText = File.ReadAllText(inputHelpersFolderPath + "\\API\\Controllers\\Controller.cs");
                                    string baseEntityFileText = File.ReadAllText(inputHelpersFolderPath + "\\API\\Model\\Common\\BaseEntity.cs");
                                    string baseEntityDTOFileText = File.ReadAllText(inputHelpersFolderPath + "\\API\\Model\\Common\\BaseEntityDto.cs");
                                    string dalFileText = File.ReadAllText(inputHelpersFolderPath + "\\API\\Model\\DAL\\FunctionName.cs");
                                    string ddlFileText = File.ReadAllText(inputHelpersFolderPath + "\\API\\Model\\DropDown\\FunctionNameDDL.cs");
                                    string dtoFileText = File.ReadAllText(inputHelpersFolderPath + "\\API\\Model\\DTO\\FunctionNameDTO.cs");
                                    string repositoryFileText = File.ReadAllText(inputHelpersFolderPath + "\\API\\Repository\\Implementation\\Repository.cs");
                                    string iRepositoryFileText = File.ReadAllText(inputHelpersFolderPath + "\\API\\Repository\\Interface\\IRepository.cs");

                                    controllerFileText = ReplaceText(controllerFileText);
                                    //baseEntityFileText = ReplaceText(baseEntityFileText);
                                    //baseEntityDTOFileText = ReplaceText(baseEntityDTOFileText);
                                    dalFileText = ReplaceText(dalFileText);
                                    ddlFileText = ReplaceText(ddlFileText);
                                    dtoFileText = ReplaceText(dtoFileText);
                                    repositoryFileText = ReplaceText(repositoryFileText);
                                    iRepositoryFileText = ReplaceText(iRepositoryFileText);

                                    WriteToFile(projectName + "\\API\\Controllers\\" + functionName + "Controller.cs", controllerFileText);
                                    WriteToFile(projectName + "\\API\\Model\\Common\\BaseEntity.cs", baseEntityFileText);
                                    WriteToFile(projectName + "\\API\\Model\\Common\\BaseEntityDTO.cs", baseEntityDTOFileText);
                                    WriteToFile(projectName + "\\API\\Model\\DAL\\" + functionName + ".cs", dalFileText);
                                    WriteToFile(projectName + "\\API\\Model\\DropDown\\" + functionName + "DDL.cs", ddlFileText);
                                    WriteToFile(projectName + "\\API\\Model\\DTO\\" + functionName + "DTO.cs", dtoFileText);
                                    WriteToFile(projectName + "\\API\\Repository\\Implementation\\" + functionName + "Repository.cs", repositoryFileText);
                                    WriteToFile(projectName + "\\API\\Repository\\Interface\\I" + functionName + "Repository.cs", iRepositoryFileText);

                                }

                                columnNamesList.Add(dr["ColumnName"] as string);
                                dataTypesList.Add(dr["DataType"] as string);
                                controlTypeList.Add(dr["ControlType"] as string);
                                lookupNameList.Add(dr["LookupName"] as string);
                                referenceTableList.Add(dr["ReferenceTable"] as string);
                                referenceColumnList.Add(dr["ReferenceColumn"] as string);



                            counter++;
                            }
                        }

                    }
                }
            }
        }

        private string ReplaceText(string source)
        {
            source = source.Replace("<<ProjectName>>", projectName);
            source = source.Replace("<<ProjectInstanceName>>", projectInstanceName);
            source = source.Replace("<<namespace>>", projectNamespace);
            source = source.Replace("<<FunctionName>>", functionName);
            source = source.Replace("<<FunctionInstanceName>>", functionNameReferenceName);
            source = source.Replace("<<RepositoryName>>", repositoryName);
            source = source.Replace("<<RepositoryInterfaceName>>", repositoryInterfaceName);
            source = source.Replace("<<RepositoryInstanceName>>", repositoryInstanceName);
            source = source.Replace("<<ControllerName>>", functionName);
            return source;
        }

        private void WriteToFile(string fileName, string fileText)
        {
            using (FileStream fs = File.Create(fileName))
            {
                byte[] info = new UTF8Encoding(true).GetBytes(fileText);
                fs.Write(info, 0, info.Length);
            }
        }

    }
}
