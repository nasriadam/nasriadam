Imports System.Data.SqlClient
Public Class Form1
    Public dr As SqlDataReader
    Public cmd As New SqlCommand
    Private Sub afficher()
        cn.Open()
        Dim listems As ListViewItem
        Dim str As String = "select * from Articles"
        cmd = New SqlCommand(str, cn)
        dr = cmd.ExecuteReader
        ListView1.Items.Clear()

        While (dr.Read)
            listems = Me.ListView1.Items.Add(dr("Reference"))
            listems.SubItems.Add(dr("Quantites"))
            listems.SubItems.Add(dr("Categories"))
            listems.SubItems.Add(dr("Affectation"))
            listems.SubItems.Add(dr("Zone de stockage"))
            listems.SubItems.Add(dr("Fournisseurs"))
            listems.SubItems.Add(dr("Ressources"))
            listems.SubItems.Add(dr("Prix"))

        End While
        dr.Close()
        cn.Close()


    End Sub
    Private Sub Fournisseurs()
        cn.Open()
        Dim str As String = "select distinct Fournisseurs from Articles"
        cmd = New SqlCommand(str, cn)
        dr = cmd.ExecuteReader
        While (dr.Read)
            Me.ComboFournisseurs.Items.Add(dr("Fournisseurs")).ToString()
        End While

        dr.Close()
        cn.Close()

    End Sub
    Private Sub Categories()
        cn.Open()
        Dim str As String = "select distinct Categories from Articles "
        cmd = New SqlCommand(str, cn)
        dr = cmd.ExecuteReader
        While (dr.Read)
            Me.ComboCategories.Items.Add(dr("Categories")).ToString()
        End While

        dr.Close()
        cn.Close()
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        afficher()
        Categories()
        Fournisseurs()

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If MsgBox("Voulez-vous ajouté un nouvel article?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo) = Windows.Forms.DialogResult.Yes Then
            cn.Open()
            cmd.CommandText = " insert into Articles values ('" & TextReference.Text & "','" & TextQuantites.Text & "','" & ComboCategories.Text & "','" & TextAffectation.Text & "','" & TextZS.Text & "','" & ComboFournisseurs.Text & "','" & TextRessources.Text & "','" & TextPrix.Text & "')"
            cmd.ExecuteNonQuery()
            MsgBox("Ajouté avec succès :)")
            cn.Close()
            afficher()


        End If
    End Sub

    Private Sub ListView1_Click(sender As Object, e As EventArgs) Handles ListView1.Click
        If ListView1.SelectedItems.Count = 0 Then
            MsgBox("Selectionner un articles!")
        Else
            cn.Open()
            cmd.CommandText = "select * from Articles where Reference = '" & ListView1.SelectedItems(0).Text.ToString & "'"
            cmd.ExecuteNonQuery()
            Dim dr As SqlDataReader = cmd.ExecuteReader()
            If dr.Read Then
                Me.TextReference.Text = dr.Item(0).ToString
                Me.TextQuantites.Text = dr.Item(1).ToString
                Me.ComboCategories.Text = dr.Item(2).ToString
                Me.TextAffectation.Text = dr.Item(3).ToString
                Me.TextZS.Text = dr.Item(4).ToString
                Me.ComboFournisseurs.Text = dr.Item(5).ToString
                Me.TextRessources.Text = dr.Item(6).ToString
                Me.TextPrix.Text = dr.Item(7).ToString
            End If
            cn.Close()
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If MsgBox("Voulez-vous modifié cet article?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo) = Windows.Forms.DialogResult.Yes Then
            cn.Open()
            Dim str As String = "update Articles set Reference = " & TextReference.Text & ", Quantites =" & TextQuantites.Text & ", Categories ='" & ComboCategories.Text & "', Affectation ='" & TextAffectation.Text & "', Zone de stockage ='" & TextZS.Text & "', Fournisseurs ='" & ComboFournisseurs.Text & "', Ressources ='" & TextRessources.Text & "', Prix ='" & TextPrix.Text & "'where Reference =(" & TextReference.Text & ")"
            cmd = New SqlCommand(str, cn)
            cmd.ExecuteNonQuery()
            MsgBox("Modifé avec succès :)")
            cn.Close()
            afficher()


        End If
    End Sub
End Class
