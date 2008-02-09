Imports System.Configuration
Imports System.IO
Imports NHibernate

Public Class HibernateDataMartMapperFactory

    Private Shared _factory As ISessionFactory

    Public Shared ReadOnly Property SessionFactory() As ISessionFactory
        Get
            If (_factory Is Nothing) Then
                Dim database As String = "PeopleSalesDM"
                Dim projectDirectory As String = ConfigurationManager.AppSettings("ProjectDirectoryPath")
                Dim filePath As String = projectDirectory & ConfigurationManager.AppSettings("HibernateMappingFilePath") & "DataMart"
                Dim config As Cfg.Configuration = New Cfg.Configuration()
                Dim cn As String = String.Format("Server=localhost;initial catalog={0};Trusted_Connection=yes", database)
                config.SetProperty("hibernate.connection.provider", "NHibernate.Connection.DriverConnectionProvider")
                config.SetProperty("hibernate.connection.driver_class", "NHibernate.Driver.SqlClientDriver")
                config.SetProperty("hibernate.connection.connection_string", cn)
                config.SetProperty("hibernate.dialect", "NHibernate.Dialect.MsSql2005Dialect")
                'config.AddAssembly("WarehouseToDataMart")
                config.AddDirectory(New DirectoryInfo(filePath))
                _factory = config.BuildSessionFactory()
            End If

            Return _factory
        End Get

    End Property

End Class