import { Injectable } from '@angular/core';

import { Page } from '@shared/page.enum';

@Injectable()
export class LinksService {

  base(link: string): string {
    return `/${link}`.replace(/\/\//g, '/');
  }

  notFound(): string {
    return this.base(Page.NotFound);
  }
}
