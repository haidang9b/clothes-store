import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { DataCategory } from 'src/models/data-category';


@Component({
  selector: 'app-dialog-category',
  templateUrl: './dialog-category.component.html',
  styleUrls: ['./dialog-category.component.scss']
})
export class DialogCategoryComponent{
  nameRequest = "";
  public categoryFormGroup : FormGroup = new FormGroup({
    name: new FormControl({value:"", disabled: false}, [Validators.required, Validators.minLength(2)])
  });
  constructor(
    public dialogRef: MatDialogRef<DialogCategoryComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DataCategory,) { 
      if( data.requestType === 1 ) { // this is for edit
        this.nameRequest = "Edit";
      }
      if(data.requestType === 2 ){ // this is for add
        this.nameRequest = "Add";
      }
  }
  
  onNoClick(): void {
    this.dialogRef.close(null);
  }
}
