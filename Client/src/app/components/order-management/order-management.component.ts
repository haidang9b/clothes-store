import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { CartService } from '../../shared/cart.service';
import { MessageService } from 'primeng/api';
import { ConfirmDialogService } from '../confirm-dialog/confirm-dialog.service';
import { MatDialog } from '@angular/material/dialog';
import { DialogExportOrderComponent } from '../dialog-export-order/dialog-export-order.component';
@Component({
  selector: 'app-order-management',
  templateUrl: './order-management.component.html',
  styleUrls: ['./order-management.component.scss']
})
export class OrderManagementComponent implements OnInit {
  orderStatus = [
    {
      id: 1,
      name: "Pending"
    },
    {
      id: 2,
      name: "Processing"
    },
    {
      id: 3,
      name: "Delivered"
    },
    {
      id: 4,
      name: "Cancelled"
    }
  ];
  orders: any[]= [];
  constructor(
    private titleService: Title,
    private cartService: CartService,
    private messageService: MessageService,
    private confirmDialogService: ConfirmDialogService,
    private dialog: MatDialog
  ) { }
  
  public getStatus(id:any){
    try{
      var status = this.orderStatus.find(x => x.id === id);
      return status === null || status === undefined ? "" : status.name;
    }catch(e){
      return "";
    }

  }
  ngOnInit(): void {
    this.titleService.setTitle("Order Management");
    this.getOrders();
  }
  EditOrder(obj: any){
    if(obj.status === 4){
      this.messageService.add({
        key: 'tr',
        severity: 'error',
        summary: 'Error',
        detail: 'Order has been cancelled',
        life:3000
      });
      return;
    }
    if(obj.status === 3){
      this.messageService.add({
        key: 'tr',
        severity: 'error',
        summary: 'Error',
        detail: 'Order has been delivered',
        life:3000
      });
      return;
    }
    this.confirmDialogService.confirmDialog({
      message: `Bạn có chắc ${this.getStatus(obj.status)} sang ${this.getStatus(obj.status + 1)}?`,
      title: 'Xác nhận chuyển trạng thái',
      confirmText: "Yes",
      cancelText: "No"
    }).subscribe(data => {
      if(data){
        this.ChangeStatus({
          id: obj.id,
          status: obj.status+1,
        });
      }
    })
  }

  private ChangeStatus(obj: any): void {
    this.cartService.changeStatusOrder({
      id: obj.id,
      status: obj.status
    }).subscribe(res => {
      if(res.isSuccess){
        this.getOrders();
        this.messageService.add({
          key: 'tr',
          severity: 'success',
          summary: 'Success',
          detail: `Chuyển trạng thái thành công`,
          life:3000
        });

      }
    });
  }

  CancelOrder(obj: any){
    if(obj.status === 3){
      this.messageService.add({
        key: 'tr',
        severity: 'error',
        summary: 'Error',
        detail: 'Order has been delivered',
        life:3000
      });
      return;
    }
    if(obj.status === 4){
      this.messageService.add({
        key: 'tr',
        severity: 'error',
        summary: 'Error',
        detail: 'Order has been cancelled',
        life:3000
      });
      return;
    }
    this.confirmDialogService.confirmDialog({
      message: `Bạn có chắc muốn hủy đơn hàng ${obj.id}?`,
      title: 'Xác nhận hủy',
      confirmText: "Yes",
      cancelText: "No"
    }).subscribe(data => {
      if(data){
        this.ChangeStatus({
          id: obj.id,
          status: 4
        });
      }
    })
  }

  getBillDetails(obj: any){
    
  }

  ExportData(){
    this.dialog.open(DialogExportOrderComponent,{
      width: "30%",
      disableClose: true
    }).afterClosed().subscribe(data =>{
      if(data == null){
        return;
      }
      else{
        this.cartService.exportOrder(data);
      }
    })
  }

  getOrders(){
    this.cartService.getOrderItems().subscribe(res => {
      if(res.isSuccess){
        this.orders = res.data;
      }
      else{
        this.orders = [];
      }
    });
  }
}
