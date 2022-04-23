import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { UploadService } from 'src/app/shared/upload.service';
import { Category } from 'src/models/category';
import { DataProduct } from 'src/models/data-product';
import { CategoryManagementService } from '../category-management/category-management.service';

@Component({
  selector: 'app-dialog-product',
  templateUrl: './dialog-product.component.html',
  styleUrls: ['./dialog-product.component.scss']
})
export class DialogProductComponent implements OnInit {
  isUploadFile: boolean = false;
  imageUrl: string ="";
  message: string = '';
  imageFile!: File;
  titleRequest: string = "";
  categories : Category[] = [new Category()];
  productFormGroup!: FormGroup;
  constructor(
    private formBuilder: FormBuilder,
    private uploadService: UploadService,
    private categoryManagementService: CategoryManagementService,
    public dialogRef: MatDialogRef<DialogProductComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DataProduct
  ) { 
    this.getCategories();
    this.productFormGroup = this.formBuilder.group({
      title: new FormControl({value: data.title},[Validators.required, Validators.minLength(2)]),
      description: new FormControl({value:data.description},[Validators.required, Validators.minLength(2)]),
      price: new FormControl({value:data.price},[Validators.required, Validators.minLength(2)]),
      quantity: new FormControl({value:data.quantity},[Validators.required, Validators.minLength(2)]),
      image: new FormControl({value:data.image},[Validators.required, Validators.minLength(2)]),
      category: new FormControl({value: data.category.id},[Validators.required])
    })
    
    if(data.requestType === 1 ) { // this is for edit
      this.titleRequest = "Edit";
      this.isUploadFile = true;
      this.imageUrl = data.image;
    }
    if(data.requestType === 2 ){ // this is for add
      this.titleRequest = "Add";
      this.isUploadFile = false;
      this.imageUrl = data.image;
    }
  }
  
  fileChangeEvent(file: File[]) {
    if(file.length === 0){
      this.message = "Please select a file";
      return;
    }
    else{
      this.message = "";
    }
    this.imageFile = file[0];
  }

  async uploadFile(){
    if(this.imageFile === undefined ){
      this.message = "Please select a file";
      return;
    }
    else{
      this.message = "";
    }
    this.uploadService.uploadFile(this.imageFile).subscribe(
      (res) => {
        if(res.isSuccess){
          this.productFormGroup.patchValue({
            image: res.data.pathFile
          })
          this.imageUrl = res.data.pathFile;
          console.log(this.imageUrl)
          this.message = "";
        }
        else{
          this.message = res.message;
        }
        
      },
      (error) => {
        this.message = "Upload failed";
      }
    );
  }
  ngOnInit(): void {
  }

  onOkClick(): void {
    let obj = {
      title: this.productFormGroup.controls['title'].value,
      description: this.productFormGroup.controls['description'].value,
      price: this.productFormGroup.controls['price'].value,
      quantity: this.productFormGroup.controls['quantity'].value,
      image: this.imageUrl,
      category: this.productFormGroup.controls['category'].value
    }
    this.dialogRef.close(obj);
  }

  onNoClick(): void {
    this.dialogRef.close(null);
  }

  getCategories(): void {
    this.categoryManagementService.getCategories().subscribe(
      (res) => {
        if(res.isSuccess){
          this.categories = res.data;
        }
        else {
          this.categories = [];
        }
      },
      (error) => {
        this.categories = [];
      }
    );
  }


}
