﻿Imports System.Data
Imports System.ComponentModel

Public Class ExternalTransactionsModel
    Inherits ExternalTransactionsAbstract
    Implements INotifyPropertyChanged

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

    Public Sub New(row As DataRow)
        Dim columnMappings = New Dictionary(Of String, String) From {
                {"ID", "ID"},
                {"TransactionDate", "TransactionDate"},
                {"Description", "Description"},
                {"ReferenceNumber", "ReferenceNumber"},
                {"Amount", "Amount"}
                }

        Try
            Apply(row, Me, columnMappings)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Public Sub New()
    End Sub

    Private Sub Apply(row As DataRow, instance As Object, columnMappings As Dictionary(Of String, String))
        For Each mapping In columnMappings
            Dim columnName = mapping.Key
            Dim propertyName = mapping.Value

            If row.Table.Columns.Contains(columnName) Then
                Dim value = row(columnName)
                Dim prop = instance.GetType().GetProperty(propertyName)

                If prop IsNot Nothing Then
                    prop.SetValue(instance, value)
                    RaisePropertyChanged(propertyName)
                End If
            End If
        Next
    End Sub

    Private Sub RaisePropertyChanged(propertyName As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
    End Sub
End Class
