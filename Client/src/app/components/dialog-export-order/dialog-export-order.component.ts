import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import * as moment from 'moment';
@Component({
  selector: 'app-dialog-export-order',
  templateUrl: './dialog-export-order.component.html',
  styleUrls: ['./dialog-export-order.component.scss']
})
export class DialogExportOrderComponent implements OnInit {
  startDate = moment().subtract(1, 'month');
  endDate = moment();
  range = new FormGroup({
    start: new FormControl({value: this.startDate}, [Validators.required]),
    end: new FormControl({value: this.endDate}, [Validators.required]),
  });
  constructor(
    public dialogRef: MatDialogRef<DialogExportOrderComponent>,
  ) { }

  ngOnInit(): void {
  }

  onOkClick(): void {
    this.dialogRef.close({start: moment(this.range.controls['start'].value).format(), end: moment(this.range.controls['end'].value).format()});
  }

  onNoClick(): void {
    this.dialogRef.close(null);
  }

}
