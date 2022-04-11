import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-error',
  template: `
    <p
      style="display: block; margin-top: 10vh; margin-bottom: 40vh; height: 16vh"
      class="alert alert-danger"
    >
      Error 404
    </p>
  `,
})
export class ErrorComponent implements OnInit {
  constructor() {}

  ngOnInit(): void {}
}
