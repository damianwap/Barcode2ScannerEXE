Imports System
Imports System.IO
Imports System.Reflection

Public Module EmbeddedLoader
    Private _initialized As Boolean = False
    Private _lock As New Object()

    Public Sub EnsureInitialized()
        SyncLock _lock
            If _initialized Then Return
            AddHandler AppDomain.CurrentDomain.AssemblyResolve, AddressOf OnAssemblyResolve
            _initialized = True
        End SyncLock
    End Sub

    Private Function OnAssemblyResolve(sender As Object, args As ResolveEventArgs) As Assembly
        Try
            Dim requested = New AssemblyName(args.Name)
            If Not requested.Name.Equals("QRCoder", StringComparison.OrdinalIgnoreCase) Then Return Nothing
            Dim asm = Assembly.GetExecutingAssembly()
            Using stream = asm.GetManifestResourceStream("ScanKilat.Libs.QRCoder.dll")
                If stream Is Nothing Then Return Nothing
                Dim raw(CInt(stream.Length) - 1) As Byte
                stream.Read(raw, 0, raw.Length)
                Return Assembly.Load(raw)
            End Using
        Catch
            Return Nothing
        End Try
    End Function
End Module
