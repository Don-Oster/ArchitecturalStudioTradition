import { Injectable } from '@angular/core';
import { Title } from '@angular/platform-browser';

import { PageMetaModel } from '@shared/models/page-meta.model';

import { BaseMetaTagsService } from './base-meta-tags.service';

@Injectable()
export class MetaTagsService {

  constructor(
    private readonly titleService: Title,
    private readonly baseMetaService: BaseMetaTagsService
  ) { }

  setTags(model: PageMetaModel) {
    this.setConstTags();
    this.setTitle(model.title);
    this.setDescription(model.description);
    this.setUrl(model.url);
    this.setImage(model.imageUrl);
    this.setRobots(model.isPrivate);
  }

  private setConstTags() {
    this.baseMetaService.setMetaTag('og:type', 'website');
    this.baseMetaService.setMetaTag('og:site_name', 'Architectural Studio Tradition');
  }

  private setTitle(value: string) {
    this.titleService.setTitle(value);
    this.baseMetaService.setMetaTag('og:title', value);
  }

  private setDescription(value: string) {
    this.baseMetaService.setMetaTag('description', value);
    this.baseMetaService.setMetaTag('og:description', value);
  }

  private setUrl(value: string) {
    this.baseMetaService.setCanonicalLink(value);
    this.baseMetaService.setMetaTag('og:url', value);
  }

  private setImage(value: string) {
    this.baseMetaService.setMetaTag('og:image', value);
  }

  private setRobots(isPrivate: boolean) {
    if (isPrivate) {
      this.baseMetaService.setMetaTag('robots', 'noindex, nofollow');
    } else {
      this.baseMetaService.setMetaTag('robots', 'index, follow');
    }
  }

}
