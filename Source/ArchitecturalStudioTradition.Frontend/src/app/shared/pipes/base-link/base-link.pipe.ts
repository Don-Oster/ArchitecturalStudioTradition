import { Pipe, PipeTransform } from '@angular/core';

import { LinksService } from '@shared/services/links/links.service';

@Pipe({
  name: 'baseLink'
})
export class BaseLinkPipe implements PipeTransform {

  constructor(
    private readonly linksService: LinksService
  ) { }

  transform(link: any, args?: any): any {
    return this.linksService.base(link);
  }
}
