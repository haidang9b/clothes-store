import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
@Component({
  selector: 'app-store',
  templateUrl: './store.component.html',
  styleUrls: ['./store.component.scss']
})
export class StoreComponent implements OnInit {
  stores : any = [];
  cities : any = [];
  constructor(
    private titleService: Title
  ) {
    this.titleService.setTitle('Store');
   }


  ngOnInit(): void {
    this.cities = [
      {
        id: 1,
        name: 'Ho Chi Minh City'
      },
      {
        id: 2,
        name: 'Nha Trang City'
      },
      {
        id: 3,
        name: 'Bao Loc City'
      },
      {
        id: 4,
        name: 'Ca Mau City'
      },
      
    ];
    this.stores = [
      {
        id: 1,
        name: 'Store 1',
        address: "19, Nguyen Huu Tho Street, Tan Phong Ward, District 7, Ho Chi Minh City",
        city: 1,
      },
      {
        id: 2,
        name: 'Store 2',
        address: "98, Ngo Tat To Street, Ward 12, Binh Thanh District, Ho Chi Minh City",
        city: 1,
      },
      {
        id: 3,
        name: 'Store 3',
        address: "22, Nguyen Dinh Chieu Street, Phuoc Vinh Ward, Nha Trang City",
        city: 2
      },
      {
        id: 4,
        name: 'Store 4',
        address: "Nguyen Tuan Street, Loc Tien Ward, Bao Loc City",
        city: 3
      },
      {
        id: 5,
        name: 'Store 5',
        address: "Mau Than Street, 6, Ward 9, Ca Mau City",
        city: 4
      }
    ]

  }

}
