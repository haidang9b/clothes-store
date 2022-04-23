import { Component, OnInit } from '@angular/core';
import { CartService } from 'src/app/shared/cart.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Title } from '@angular/platform-browser';
@Component({
  selector: 'app-tracking',
  templateUrl: './tracking.component.html',
  styleUrls: ['./tracking.component.scss'],
})
export class TrackingComponent implements OnInit {
  totalPrice = 0;
  order!: any;
  statusTracking = 4;
  statusName = '';
  billDetails!: any;
  orderStatus = [
    {
      id: 1,
      name: 'Pending',
    },
    {
      id: 2,
      name: 'Processing',
    },
    {
      id: 3,
      name: 'Delivered',
    },
    {
      id: 4,
      name: 'Cancelled',
    },
  ];
  constructor(
    private cartService: CartService,
    private route: ActivatedRoute,
    private router: Router,
    private titleService: Title
  ) {}

  ngOnInit(): void {
    this.getOrderFromRoute();
  }
  public getStatus(id: any) {
    try {
      var status = this.orderStatus.find((x) => x.id === id);
      return status === null || status === undefined ? '' : status.name;
    } catch (e) {
      return '';
    }
  }

  getOrderFromRoute() {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.cartService.getOrderItemByID(id).subscribe((res) => {
      if (res.isSuccess) {
        this.order = res.data;
        this.titleService.setTitle(`Tracking Order - ${this.order.id}`);
        this.statusTracking = this.order.status;
        this.billDetails = this.order.billDetails;
        this.totalPrice = this.getTotalPrice(this.billDetails);
        this.statusName = this.getStatus(this.order.status);
      } else {
        this.router.navigate(['/error']);
      }
    });
  }

  getTotalPrice(obj: any) {
    var total = 0;
    obj.forEach(function (element: any) {
      total += element.product.price;
    });
    return total;
  }
}
