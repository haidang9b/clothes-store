import { Component, OnInit } from '@angular/core';
import { MessageService } from 'primeng/api';

@Component({
  selector: 'app-custom-toast',
  templateUrl: './custom-toast.component.html',
  styleUrls: ['./custom-toast.component.scss'],
  providers: []
})
export class CustomToastComponent implements OnInit {

  constructor(
    private messageService: MessageService
  ) { }

  ngOnInit(): void {
  }
  onConfirm() {
    this.messageService.clear('c');
  }

  onReject() {
      this.messageService.clear('c');
  }

  clear() {
      this.messageService.clear();
  }
}
