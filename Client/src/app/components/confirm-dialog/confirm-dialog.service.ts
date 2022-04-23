import { Injectable } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Observable } from 'rxjs';
import { ConfirmDialogData } from 'src/models/confirm-dialog-data';
import { ConfirmDialogComponent } from './confirm-dialog.component';

@Injectable({
  providedIn: 'root'
})
export class ConfirmDialogService {
  
  constructor(private dialog: MatDialog) { }
  confirmDialog(data: ConfirmDialogData): Observable<boolean> {
    return this.dialog.open(ConfirmDialogComponent, {
      data,
      width: "400px",
      disableClose: true
    }).afterClosed();
  }
}
