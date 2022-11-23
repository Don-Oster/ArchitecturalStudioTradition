import { Inject, Injectable, Optional } from '@angular/core';
import { DOCUMENT } from '@angular/common';

import { PageMetaModel } from '@shared/models/page-meta.model';
import { PAGE_META, PAGE_META_DEFAULT_TITLE } from '@shared/models/page-meta.consts';
import { Page } from '@shared/page.enum';

import { LinksService } from '../links/links.service';
import { MetaTagsService } from './meta-tags.service';
import { META_BASE_URL_TOKEN } from './meta-base-url.token';

@Injectable()
export class PageMetaService {
  private readonly absoluteUrl;

  constructor(
    private readonly linksService: LinksService,
    private readonly metaTags: MetaTagsService,
    @Inject(META_BASE_URL_TOKEN) @Optional() private readonly baseUrl: string,
    @Inject(DOCUMENT) private readonly document: Document
  ) {
    this.absoluteUrl = this.baseUrl ? this.baseUrl.trim() : this.document.location.origin;
  }

  forHome() {
    const meta: PageMetaModel = {
      title: PAGE_META.HOME.title,
      description: PAGE_META.HOME.description,
      url: this.linksService.base(''),
      imageUrl: PAGE_META.HOME.imageUrl,
      isPrivate: false
    };

    this.setTags(meta);
  }

  forSearch() {
    const meta: PageMetaModel = {
      title: PAGE_META.SEARCH.title,
      description: PAGE_META.SEARCH.description,
      url: this.linksService.base(Page.Search),
      imageUrl: PAGE_META.SEARCH.imageUrl,
      isPrivate: true
    };

    this.setTags(meta);
  }

  forAbout() {
    const meta: PageMetaModel = {
      title: PAGE_META.ABOUT.title,
      description: PAGE_META.ABOUT.description,
      url: this.linksService.base(Page.About),
      imageUrl: PAGE_META.ABOUT.imageUrl,
      isPrivate: false
    };

    this.setTags(meta);
  }

  forNotFound() {
    const meta: PageMetaModel = {
      title: PAGE_META.NOT_FOUND.title,
      description: PAGE_META.NOT_FOUND.description,
      url: this.linksService.notFound(),
      imageUrl: PAGE_META.NOT_FOUND.imageUrl,
      isPrivate: true
    };

    this.setTags(meta);
  }

  forTermsConditions() {
    const meta: PageMetaModel = {
      title: PAGE_META.TERMS_AND_CONDITIONS.title,
      description: PAGE_META.TERMS_AND_CONDITIONS.description,
      url: this.linksService.base(Page.TermsConditions),
      imageUrl: PAGE_META.TERMS_AND_CONDITIONS.imageUrl,
      isPrivate: false
    };

    this.setTags(meta);
  }

  private setTags(meta: PageMetaModel) {
    meta.title = meta.title !== PAGE_META_DEFAULT_TITLE ? `${meta.title} | ${PAGE_META_DEFAULT_TITLE}` : meta.title;
    meta.url = `${this.absoluteUrl}${meta.url}`;

    this.metaTags.setTags(meta);
  }
}
