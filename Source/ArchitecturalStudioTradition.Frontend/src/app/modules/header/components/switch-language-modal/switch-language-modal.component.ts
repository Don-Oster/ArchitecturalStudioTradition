import { Component } from '@angular/core';

@Component({
  selector: 'app-switch-language-modal',
  templateUrl: './switch-language-modal.component.html',
  styleUrls: ['./switch-language-modal.component.scss']
})
export class SwitchLanguageModalComponent {

  public items = [
    {
      text: 'English'
    },
    {
      text: 'Russian'
    }
  ];

  close(): void {
  }
}
