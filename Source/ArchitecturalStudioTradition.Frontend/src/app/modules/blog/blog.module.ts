import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';

import { InfiniteScrollModule } from 'ngx-infinite-scroll';

import { BlogComponent } from './blog.component';

@NgModule({
  imports: [
    CommonModule,
    HttpClientModule,
    InfiniteScrollModule
  ],
  exports: [BlogComponent],
  declarations: [BlogComponent],
})
export class BlogModule { }
